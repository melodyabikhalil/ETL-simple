﻿using ETL.UI;
using ETL.Utility;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using static System.Net.Mime.MediaTypeNames;

namespace ETL.Core
{
    public class DSpaceHelper
    {
        public static DataTable ConvertCSVtoDataTable(string strFilePath)
        {
            StreamReader sr = new StreamReader(strFilePath);
            string[] headers = sr.ReadLine().Split(',');
            DataTable dt = new DataTable();
            foreach (string header in headers)
            {
                dt.Columns.Add(header);
            }
            while (!sr.EndOfStream)
            {
                string[] row = sr.ReadLine().Split(',');
                DataRow dr = dt.NewRow();
                for (int i = 0; i < row.Count(); i++)
                {
                    dr[headers[i]] = row[i];
                }
                dt.Rows.Add(dr);
            }
            return dt;
        }

        public static List<DSpaceMetadataField> CreateMetadataFieldsList(DataTable dataTable)
        {
            DSpaceMetadataField dSpaceMetadataField;
            List<DSpaceMetadataField> dSpaceMetadataFields = new List<DSpaceMetadataField>();
            foreach (DataRow row in dataTable.Rows)
            {
                string element = row.Field<string>("element");
                string qualifier = "";
                try
                {
                    qualifier = row.Field<string>("qualifier");
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    Helper.Log(e.Message, "Dspace-CreateMetadataList");
                }
                dSpaceMetadataField = new DSpaceMetadataField(element, qualifier);
                dSpaceMetadataFields.Add(dSpaceMetadataField);
            }
            return dSpaceMetadataFields;
        }

        public static List<DSpaceMetadataField> GetMetadataFields()
        {
            string applicationLocation = System.Reflection.Assembly.GetEntryAssembly().Location;
            string applicationDirectory = Path.GetDirectoryName(applicationLocation);

            string path = @"" + applicationDirectory + "\\Resources\\dspace_metadata.csv";
            DataTable dt = ConvertCSVtoDataTable(path);
            List<DSpaceMetadataField> fields = CreateMetadataFieldsList(dt);
            return fields;
        }

        public static void CreateXml(List<KeyValuePair<DSpaceMetadataField, string>> metadatasWithValues, string path)
        {
            XmlDocument xmlDoc = new XmlDocument();
            XmlNode rootNode = xmlDoc.CreateElement("dublin_core");
            xmlDoc.AppendChild(rootNode);

            foreach (KeyValuePair<DSpaceMetadataField, string> metadataWithValue in metadatasWithValues)
            {
                XmlNode node = xmlDoc.CreateElement("dcvalue");

                DSpaceMetadataField field = metadataWithValue.Key;
                string value = metadataWithValue.Value;
                AddAttributeToNode("schema", node, xmlDoc, "dc");
                AddAttributeToNode("element", node, xmlDoc, field.element);
                if (field.qualifier != null && field.qualifier != "")
                {
                    AddAttributeToNode("qualifier", node, xmlDoc, field.qualifier);
                }

                node.InnerText = value;
                rootNode.AppendChild(node);
            }
            xmlDoc.Save(path + "\\dublin_core.xml");
        }

        private static void AddAttributeToNode(string attributeName, XmlNode node, XmlDocument document, string value)
        {
            XmlAttribute attribute = document.CreateAttribute(attributeName);
            attribute.Value = value;
            node.Attributes.Append(attribute);
        }

        public static void CreateItemsRepository(DataTable dspaceData, string repositoryPath)
        {
            Directory.CreateDirectory(repositoryPath);
            int itemNumber = 0;

            Global.progressForm.UpdateForm(ProgressForm.PROGRESSBAR_MAXIMUM, dspaceData.Rows.Count.ToString());
            foreach (DataRow dataRow in dspaceData.Rows)
            {
                Global.progressForm.UpdateForm(ProgressForm.PROGRESSBAR_VALUE, itemNumber.ToString());
                List<KeyValuePair<DSpaceMetadataField, string>> metadataWithValues = new List<KeyValuePair<DSpaceMetadataField, string>>();
                string itemRepository = "item" + itemNumber.ToString();
                itemNumber++;
                DSpaceMetadataField metadataField;
                string itemCompletePath = repositoryPath + "\\" + itemRepository;
                Directory.CreateDirectory(itemCompletePath);

                for (int i = 0; i < dspaceData.Columns.Count; i++)
                {
                    string column = dspaceData.Columns[i].ColumnName;
                    string value = dataRow.Field<string>(column);
                    if (value != null)
                    {
                        if (column == "path")
                        {
                            DownloadResource(value, itemCompletePath);
                        }
                        else
                        {
                            string[] metadatafields = column.Split('.');
                            string element = metadatafields[0];
                            string qualifier = "";
                            if (metadatafields.Count() == 2)
                            {
                                qualifier = metadatafields[1];
                            }
                            metadataField = new DSpaceMetadataField(element, qualifier);
                            metadataWithValues.Add(new KeyValuePair<DSpaceMetadataField, string>(metadataField, value));
                        }
                    }
                }
                CreateXml(metadataWithValues, itemCompletePath);
            }
        }

        public static void DownloadResource(string resourcePath, string downloadPath)
        {
            Path.GetFullPath(resourcePath).Replace(@"\", @"\\");
            Path.GetFullPath(downloadPath).Replace(@"\", @"\\");
            string resourceName = Path.GetFileName(resourcePath).Trim();
            try
            {
                System.IO.File.Copy(resourcePath, downloadPath + @"\" + resourceName, true);
                CreateContentsFile(downloadPath, resourceName);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Helper.Log(e.Message, "DSpace-DownloadResource");
            }
        }

        private static void CreateContentsFile(string path, string resourceName)
        {
            using (StreamWriter sw = File.CreateText(path + "\\contents."))
            {
                sw.WriteLine(resourceName);
            }
        }
    }
}
