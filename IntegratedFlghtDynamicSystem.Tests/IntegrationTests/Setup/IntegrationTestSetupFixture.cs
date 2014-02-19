using System;
using System.Data.Entity;
using System.Data.EntityClient;
using System.Data.Linq;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using IntegratedFlghtDynamicSystem.Global;
using IntegratedFlghtDynamicSystem.Models;
using IntegratedFlghtDynamicSystem.Models.DataTools;
using IntegratedFlghtDynamicSystem.Tests.Controllers;
using Ninject;
using NUnit.Framework;

namespace IntegratedFlghtDynamicSystem.Tests.IntegrationTests
{
    [SetUpFixture]
    public class IntegrationTestSetupFixture : UnitTestSetupFixture
    {
        public class FileListRestore
        {
            public string LogicalName { get; set; }

            public string Type { get; set; }
        }

        protected static bool RemoveDbAfter = false;
        protected static string NameDb = "ISFDS";
        protected static string TestDbName = "ISFDSTest";

        protected override void InitRepository(StandardKernel kernel)
        {
            FileInfo sandboxFile;
            const string connectionStringName = "ISFDSTestEntities";
            //CopyDb(kernel, out sandboxFile, out connectionString);
            kernel.Bind<IUnitOfWork>().To<UnitOfWork>().InTransientScope().WithConstructorArgument("connectionStringName", connectionStringName);
        }

        private void CopyDb(StandardKernel kernel, out FileInfo sandboxFile)
        {
            var config = kernel.Get<IConfig>();
            var db = new DataContext(config.ConnectionStrings("ISFDSDB"));
            sandboxFile = new FileInfo(string.Format("{0}\\{1}.bak", Sandbox, TestDbName));
            var sandboxDir = new DirectoryInfo(Sandbox);

            //backupFile
            var textBackUp = string.Format(@"-- Backup the database
            BACKUP DATABASE [{0}]
            TO DISK = '{1}'
            WITH COPY_ONLY",
            NameDb, sandboxFile.FullName);

            db.ExecuteCommand(textBackUp);

            var restoreFileList = string.Format("RESTORE FILELISTONLY FROM DISK = '{0}'", sandboxFile.FullName);
            var fileListRestores = db.ExecuteQuery<FileListRestore>(restoreFileList).ToList();
            var logicalDbName = fileListRestores.FirstOrDefault(p => p.Type == "D");
            var logicalLogDbName = fileListRestores.FirstOrDefault(p => p.Type == "L");

            var restoreDb = string.Format("RESTORE DATABASE [{0}] FROM DISK = '{1}' WITH FILE = 1, MOVE N'{2}' TO N'{4}\\{0}.mdf', MOVE N'{3}' TO N'{4}\\{0}.ldf', NOUNLOAD, STATS = 10",
                TestDbName, sandboxFile.FullName, logicalDbName.LogicalName, logicalLogDbName.LogicalName, sandboxDir.FullName);
            db.ExecuteCommand(restoreDb);
        }


        [TearDown]
        public void TearDown()
        {
            if (RemoveDbAfter)
            {
                RemoveDb();
            }
        }

        private void RemoveDb()
        {
            var config = DependencyResolver.Current.GetService<IConfig>();

            var db = new DataContext(config.ConnectionStrings("ConnectionString"));

            var textCloseConnectionTestDb = string.Format(@"ALTER DATABASE [{0}] SET SINGLE_USER WITH ROLLBACK IMMEDIATE", TestDbName);
            db.ExecuteCommand(textCloseConnectionTestDb);

            var textDropTestDb = string.Format(@"DROP DATABASE [{0}]", TestDbName);
            db.ExecuteCommand(textDropTestDb);
        }
    }
}