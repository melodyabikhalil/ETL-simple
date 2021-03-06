﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ETL.Core;
using System.Data;

namespace ETLTest
{
    [TestClass]
    public class UnitTestExpression
    {
        private DataTable sourceDatatable = new DataTable();
        private DataTable destinationDatatable = new DataTable();
        private DataTable expressionDatatable = new DataTable();
        private DataTable mappingDatatable = new DataTable();
        private DataTable expectedDatatable = new DataTable();

        private void SetupDatatTables()
        {
            sourceDatatable.Columns.Add("id", typeof(int));
            sourceDatatable.Columns.Add("fullname", typeof(string));
            sourceDatatable.Columns.Add("dob", typeof(DateTime));
            sourceDatatable.Columns.Add("credit", typeof(decimal));
            sourceDatatable.Columns.Add("gender", typeof(char));

            destinationDatatable.Columns.Add("id", typeof(int));
            destinationDatatable.Columns.Add("firstname", typeof(string));
            destinationDatatable.Columns.Add("lastname", typeof(string));
            destinationDatatable.Columns.Add("dateofbirth", typeof(DateTime));
            destinationDatatable.Columns.Add("credit", typeof(decimal));
            destinationDatatable.Columns.Add("sex", typeof(string));
            destinationDatatable.TableName = "Users";

            expressionDatatable.Columns.Add("TableNameDest", typeof(string));
            expressionDatatable.Columns.Add("ColumnDest", typeof(string));
            expressionDatatable.Columns.Add("ExpressionType", typeof(string));
            expressionDatatable.Columns.Add("MapColumnName", typeof(string));
            expressionDatatable.Columns.Add("Expression", typeof(string));
            expressionDatatable.Columns.Add("RegularExpression", typeof(string));
            expressionDatatable.Columns.Add("MappingName", typeof(string));

            mappingDatatable.Columns.Add("MappingName", typeof(string));
            mappingDatatable.Columns.Add("FromValue", typeof(string));
            mappingDatatable.Columns.Add("ToValue", typeof(string));

            sourceDatatable.Rows.Add(1, "Jenna Smith", "1997-09-03", 13.45, "F");
            sourceDatatable.Rows.Add(2, "John Doe", "2001-05-03", 150.5, "M");
            sourceDatatable.Rows.Add(3, "Michael Doe", null, 0, "M");

            expressionDatatable.Rows.Add("Users", "id", "Replace", null, "[id]", null, null);
            expressionDatatable.Rows.Add("Users", "firstname", "Reg", null, "[fullname]", "([^\\s]+)", null);
            expressionDatatable.Rows.Add("Users", "lastname", "Reg", null, "[fullname]", "(?<=\\s).*", null);
            expressionDatatable.Rows.Add("Users", "credit", "Replace", null, "[credit]", null, null);
            expressionDatatable.Rows.Add("Users", "dateofbirth", "Replace", null, "[dob]", null, null);
            expressionDatatable.Rows.Add("Users", "sex", "Map", "gender", null, null, "Gender");

            mappingDatatable.Rows.Add("Gender", "F", "Female");
            mappingDatatable.Rows.Add("Gender", "M", "Male");


            expectedDatatable.Columns.Add("id", typeof(int));
            expectedDatatable.Columns.Add("firstname", typeof(string));
            expectedDatatable.Columns.Add("lastname", typeof(string));
            expectedDatatable.Columns.Add("dateofbirth", typeof(DateTime));
            expectedDatatable.Columns.Add("credit", typeof(decimal));
            expectedDatatable.Columns.Add("sex", typeof(string));
            expectedDatatable.TableName = "Users";

            expectedDatatable.Rows.Add(1, "Jenna", "Smith", "1997-09-03", 13.45, "Female");
            expectedDatatable.Rows.Add(2, "John", "Doe", "2001-05-03", 150.5, "Male");
            expectedDatatable.Rows.Add(3, "Michael", "Doe", "1/1/0001 12:00:00 AM", 0, "Male");
        }

        [TestMethod]
        public void TestCreateDestinationDatatable()
        {
            SetupDatatTables();
            bool success = Expression.AddValuesToDatatableDestination(sourceDatatable, destinationDatatable, expressionDatatable, mappingDatatable);

            Assert.IsTrue(success);
            Assert.AreEqual(destinationDatatable.Rows.Count, 3);

            for (int i = 0; i < destinationDatatable.Rows.Count; i++)
            {
                for (int c = 0; c < destinationDatatable.Columns.Count; c++)
                {
                    var destinationValue = destinationDatatable.Rows[i][c];
                    var expectedValue = expectedDatatable.Rows[i][c];
                    Assert.AreEqual(destinationValue, expectedValue);
                }
            }
        }

        [TestMethod]
        public void TestReplace()
        {
            SetupDatatTables();
            DataRow row = sourceDatatable.Rows[0];
            string expression = "[fullname]";
            string result = Expression.Replace(expression, row);
            string expectedResult = "Jenna Smith";
            Assert.AreEqual(expectedResult, result);
        }

        [TestMethod]
        public void TestRegularExpression()
        {
            SetupDatatTables();
            DataRow row = sourceDatatable.Rows[0];
            string text = "Jenna Smith";
            string expression = "([^\\s]+)";
            string result = Expression.Regexp(expression, row, text);
            string expectedResult = "Jenna";
            Assert.AreEqual(expectedResult, result);
        }

        [TestMethod]
        public void TestMap()
        {
            SetupDatatTables();
            DataRow row = sourceDatatable.Rows[0];
            DataRow expressionRow = expressionDatatable.Rows[5];
            string result = Expression.GetValue(expressionRow, row, mappingDatatable);
            string expectedResult = "Female";
            Assert.AreEqual(expectedResult, result);
        }
    }
}
