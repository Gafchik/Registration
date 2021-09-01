using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Data.SqlClient;
using Dapper;
using System.Data;

// ПРИМЕЧАНИЕ. Команду "Переименовать" в меню "Реструктуризация" можно использовать для одновременного изменения имени класса "Service" в коде, SVC-файле и файле конфигурации.
public class Service : IService
{
    string conSTR = "Data Source=SQL5080.site4now.net;Initial Catalog=db_a7920a_mkonjibhu;User Id=db_a7920a_mkonjibhu_admin;Password=QwertyuioP123";
    public string Reg(User new_user)
    {
        string rez = string.Empty;   
        using (IDbConnection db = new SqlConnection(conSTR))
        {
            db.Open();
            using (var transaction = db.BeginTransaction())
            {
                try
                {
                    var sqlQuery = "exec [AddUser] @login , @name,@pass_hash ";
                    var values = new { login = new_user.Login,
                        name =new_user.Name,
                        pass_hash = new_user.Pass.GetHashCode() };
                    db.Query(sqlQuery, values, transaction);
                    transaction.Commit();
                    rez = "done";
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    rez = ex.Message;
                }
            }
        }
        return rez;
    }

    public string SingIn(string login, string pass)
    {
        string rez = string.Empty;
        using (IDbConnection db = new SqlConnection(conSTR))
        {
            db.Open();
            using (var transaction = db.BeginTransaction())
            {
                try
                {
                    var sqlQuery = "exec [SingIn] @login,@pass_hash ";
                    var values = new
                    {
                        login = login,
                        pass_hash = pass.GetHashCode()
                    };
                    int count = db.Query<int>(sqlQuery, values, transaction).ToList()[0];
                    transaction.Commit();
                    if (count == 1)
                        rez = "done";
                    else
                        rez = "not found";
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    rez = ex.Message;
                }
            }
        }
        return rez;
    }
}
