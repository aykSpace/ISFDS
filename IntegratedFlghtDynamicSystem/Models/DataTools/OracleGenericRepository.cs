using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using Oracle.DataAccess.Client;
using IntegratedFlghtDynamicSystem.Extensions;

namespace IntegratedFlghtDynamicSystem.Models.DataTools
{
    public class OracleNuRepository : IRepository<NU>
    {

        private readonly string _connectionString = ConfigurationManager.ConnectionStrings["mkcdc2"].ConnectionString;

        IEnumerable<NU> IRepository<NU>.Get()
        {
            const string queryString = @"SELECT * FROM (select  ID_NU, N_NU, X, Y, Z, VX, VY, VZ, T_NU_DMB, VITOK, SB, COMMENT_NU, NAME_KA from star.nu where NAME_KA = 'СМ' order by ID_NU desc)
                                         where rownum <= 10";
            try
            {
                using (var ocon = new OracleConnection(_connectionString))
                {
                    var orcCommand = new OracleCommand(queryString, ocon);
                    var dTable = new DataTable();
                    ocon.Open();
                    dTable.Load(orcCommand.ExecuteReader());
                    var nu = new List<NU>(10);
                    using (DataTableReader dtReader = dTable.CreateDataReader())
                    {
                        while (dtReader.Read())
                        {
                            var getT = (Convert.ToDateTime(dtReader.GetValue(8)).GetTimeSecond());

                            nu.Add(new NU
                            {
                                ID_NU = Convert.ToInt32(dtReader.GetValue(0)),
                                N_NU = Convert.ToInt32(dtReader.GetValue(1)),
                                X = Convert.ToDouble(dtReader.GetValue(2)),
                                Y = Convert.ToDouble(dtReader.GetValue(3)),
                                Z = Convert.ToDouble(dtReader.GetValue(4)),
                                VX = Convert.ToDouble(dtReader.GetValue(5)),
                                VY = Convert.ToDouble(dtReader.GetValue(6)),
                                VZ = Convert.ToDouble(dtReader.GetValue(7)),
                                DateNU = Convert.ToDateTime(dtReader.GetValue(8)),
                                Vitok = Convert.ToInt32(dtReader.GetValue(9)),
                                Sbal = Convert.ToDouble(dtReader.GetValue(10)),
                                t = getT
                            });
                        }
                    }
                    return nu;
                }
            }
            catch (DataException exception)
            {
                throw new DataException(exception.Message);
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }

        public NU GetById(object id)
        {
            string queryString = @"select ID_NU, N_NU, X, Y, Z, VX, VY, VZ, T_NU_DMB, VITOK, SB, COMMENT_NU, NAME_KA from star.nu where NAME_KA = 'СМ' and ID_NU = "+id+"";
            try
            {
                using (var ocon = new OracleConnection(_connectionString))
                {
                    var orcCommand = new OracleCommand(queryString, ocon);
                    var dTable = new DataTable();
                    ocon.Open();
                    dTable.Load(orcCommand.ExecuteReader());
                    using (DataTableReader dtReader = dTable.CreateDataReader())
                    {
                        NU nu = null;
                        while (dtReader.Read())
                        {
                            var getT = (Convert.ToDateTime(dtReader.GetValue(8)).GetTimeSecond());

                            nu = (new NU
                            {
                                ID_NU = Convert.ToInt32(dtReader.GetValue(0)),
                                N_NU = Convert.ToInt32(dtReader.GetValue(1)),
                                X = Convert.ToDouble(dtReader.GetValue(2)),
                                Y = Convert.ToDouble(dtReader.GetValue(3)),
                                Z = Convert.ToDouble(dtReader.GetValue(4)),
                                VX = Convert.ToDouble(dtReader.GetValue(5)),
                                VY = Convert.ToDouble(dtReader.GetValue(6)),
                                VZ = Convert.ToDouble(dtReader.GetValue(7)),
                                DateNU = Convert.ToDateTime(dtReader.GetValue(8)),
                                Vitok = Convert.ToInt32(dtReader.GetValue(9)),
                                Sbal = Convert.ToDouble(dtReader.GetValue(10)),
                                t = getT
                            });
                        }
                        return nu;
                    }
                }
            }
            catch (DataException exception)
            {
                throw new DataException(exception.Message);
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }

        public void Insert(NU entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(object id)
        {
            throw new NotImplementedException();
        }

        public void Delete(NU entityToDelete)
        {
            throw new NotImplementedException();
        }

        public void Update(NU entityToUpdate)
        {
            throw new NotImplementedException();
        }
    }
}