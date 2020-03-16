﻿using ETL.Core;
using ETL.Utility;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ETL.UI
{
    public partial class NewDatabaseForm : Form
    {
        public NewDatabaseForm()
        {
            InitializeComponent();
            this.CenterToParent();
            this.credentialsTabPage.Enabled = false;
            this.AcceptButton = connectButton;
        }

        private void ConnectToDb(int dbType)
        {
            Database database;
            switch (dbType)
            {
                case 0:
                    database = this.CreateMysqlDatabase();
                    break;
                case 1:
                    database = this.CreateSQLServerDatabase();
                    break;
                case 2:
                    database = this.CreatePostgresDatabase();
                    break;
                case 3:
                    database = this.CreateAccessDatabase();
                    break;
                case 4:
                    database = this.CreateODBCDatabase();
                    break;
                default:
                    database = this.CreateMysqlDatabase();
                    break;
            }
            bool connected = database.Connect();
            if (connected)
            {
                database.tablesNames = database.GetTablesNames();
                database.CreateTablesList(database.tablesNames);
                database.GetQueriesNames();
                // TODO: add database.queries from json file (if they exist);
                if (Global.DatabaseAlreadyConnected(database))
                {
                    this.ShowErrorDialogAndClose();
                }
                Global.Databases.Add(database);
                this.AddNodesToTreeView(database);
                ETLParent.ShowMainContainer();
                this.Close();
            }
            else
            {
                var pressed = MessageBox.Show("Could not connect to database", "Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
                if (pressed == DialogResult.Cancel)
                {
                    this.Close();
                }
            }
        }

        private MySQLDatabase CreateMysqlDatabase()
        {
            string databaseName = dbNameTextBox.Text;
            string username = usernameTextBox.Text;
            string password = passTextBox.Text;
            string serverName = hostTextBox.Text;
            MySQLDatabase mySQLDatabase = new MySQLDatabase(serverName, username, password, databaseName);
            return mySQLDatabase;
        }

        private PostgreSQLDatabase CreatePostgresDatabase()
        {
            string databaseName = dbNameTextBox.Text;
            string username = usernameTextBox.Text;
            string password = passTextBox.Text;
            string serverName = hostTextBox.Text;
            string schema = schemaTextBox.Text;
            string port = portTextBox.Text;
            PostgreSQLDatabase postgreSQLDatabase = new PostgreSQLDatabase(serverName, username, password, databaseName, port, schema);
            return postgreSQLDatabase;
        }

        private SQLServerDatabase CreateSQLServerDatabase()
        {
            string databaseName = dbNameTextBox.Text;
            string username = usernameTextBox.Text;
            string password = passTextBox.Text;
            string serverName = hostTextBox.Text;
            string schema = schemaTextBox.Text;
            SQLServerDatabase sqlServerDatabase = new SQLServerDatabase(serverName, username, password, databaseName, schema);
            return sqlServerDatabase;
        }

        private AccessDatabase CreateAccessDatabase()
        {
            string path = dbNameTextBox.Text;
            AccessDatabase accessDatabase = new AccessDatabase(path);
            return accessDatabase;
        }

        private Database CreateODBCDatabase()
        {
            string connectionString = dbNameTextBox.Text;
            ODBCDatabase odbcDatabase = new ODBCDatabase(connectionString);
            return odbcDatabase;
        }

        private void ShowErrorDialogAndClose()
        {
            var okPressed = MessageBox.Show("Already connected to this database", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            if (okPressed == DialogResult.OK)
            {
                this.Close();
            }
        }

        private void AddNodesToTreeView(Database database)
        {
            TreeView treeview = ETLParent.GetTreeView();
            TreeNode node = UIHelper.AddBranch(database.databaseName, treeview);
            node.Tag = database.databaseName;
            TreeNode tablesNode = UIHelper.AddChildBranch("Tables", node.Index, treeview);
            UIHelper.AddChildrenNodes(database.tablesNames, tablesNode);

            TreeNode queriesNode = UIHelper.AddChildBranch("Queries", node.Index, treeview);
            // TODO: get queries from json file and add them as children nodes to queries node 
            //we get the queries and we set them in database.queries
            UIHelper.AddChildrenNodes(database.queriesNames, queriesNode);
        }

        private void GoToCredentialsTabFromTypeTabButton_Click(object sender, EventArgs e)
        {
            this.credentialsTabPage.Enabled = true;
            this.newDatabaseTabControl.SelectedTab = this.credentialsTabPage;
            switch (this.dbTypesComboBox.SelectedIndex)
            {

                case 0:
                    schemaLabel.Hide();
                    schemaTextBox.Hide();
                    portLabel.Hide();
                    portTextBox.Hide();
                    hostTextBox.Show();
                    hostLabel.Show();
                    usernameTextBox.Show();
                    usernameLabel.Show();
                    passTextBox.Show();
                    passwordLabel.Show();
                    dbNameLabel.Show();
                    dbNameTextBox.Show();
                    break;
                case 1:
                    schemaLabel.Show();
                    schemaTextBox.Show();
                    portLabel.Hide();
                    portTextBox.Hide();
                    hostTextBox.Show();
                    hostLabel.Show();
                    usernameTextBox.Show();
                    usernameLabel.Show();
                    passTextBox.Show();
                    passwordLabel.Show();
                    dbNameLabel.Show();
                    dbNameTextBox.Show();
                    break;
                case 2:
                    schemaLabel.Show();
                    schemaTextBox.Show();
                    portLabel.Show();
                    portTextBox.Show();
                    hostTextBox.Show();
                    hostLabel.Show();
                    usernameTextBox.Show();
                    usernameLabel.Show();
                    passTextBox.Show();
                    passwordLabel.Show();
                    dbNameLabel.Show();
                    dbNameTextBox.Show();
                    break;
                case 3:
                    hostTextBox.Hide();
                    hostLabel.Hide();
                    usernameTextBox.Hide();
                    usernameLabel.Hide();
                    passTextBox.Hide();
                    passwordLabel.Hide();
                    dbNameLabel.Text = "Path";
                    dbNameTextBox.Show();
                    portLabel.Hide();
                    portTextBox.Hide();
                    schemaLabel.Hide();
                    schemaTextBox.Hide();
                    break;
                case 4:
                    hostTextBox.Hide();
                    hostLabel.Hide();
                    usernameTextBox.Hide();
                    usernameLabel.Hide();
                    passTextBox.Hide();
                    passwordLabel.Hide();
                    dbNameLabel.Text = "Connection String";
                    dbNameTextBox.Show();
                    portLabel.Hide();
                    portTextBox.Hide();
                    schemaLabel.Hide();
                    schemaTextBox.Hide();
                    break;
            }
        }

        private void GoBackToTypeTabFromCredentialsTabButton_Click(object sender, EventArgs e)
        {
            this.newDatabaseTabControl.SelectedTab = this.typeTabPage;
        }

        private void ConnectButton_Click(object sender, EventArgs e)
        {
            this.ConnectToDb(dbTypesComboBox.SelectedIndex);
        }
    }
}
