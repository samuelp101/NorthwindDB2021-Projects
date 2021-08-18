using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Threading.Tasks;

namespace NorthwindDB2021.Data.ORM
{
    public static class SqlORM
    {
        /// <summary>
        /// Map SqlDataReader data to Object
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <param name="rdr"></param>
        /// <param name="version"></param>
        /// <returns></returns>
        //public static T MapDataReader<T>(this T obj, SqlDataReader rdr, string version = "none")
        //{
        //    PropertyInfo[] props = obj.GetType().GetProperties();
        //    string OrderByValue = String.Empty;

        //    foreach (var prop in props)
        //    {
        //        for (int i = 0; i < rdr.FieldCount; i++)
        //        {
        //            if (string.Compare(rdr.GetName(i).Replace("_", ""), prop.Name, ignoreCase: true) == 0)
        //            {
        //                string propType = String.Empty;

        //                if (prop.PropertyType.Name == "Nullable`1")
        //                {
        //                    propType = Nullable.GetUnderlyingType(prop.PropertyType).Name;
        //                }
        //                else
        //                {
        //                    propType = prop.PropertyType.Name;
        //                }

        //                switch (propType.ToLower())
        //                {
        //                    case "string":
        //                        prop.SetValue(obj, rdr.GetValue(i).ToString());
        //                        break;
        //                    case "int16":
        //                        prop.SetValue(obj, rdr.IsDBNull(i) ? default(Int16) : (Int16)rdr.GetValue(i));
        //                        break;
        //                    case "int32":
        //                        prop.SetValue(obj, rdr.IsDBNull(i) ? default(Int32) : (Int32)rdr.GetValue(i));
        //                        break;
        //                    case "int64":
        //                        prop.SetValue(obj, rdr.IsDBNull(i) ? default(Int64) : (Int64)rdr.GetValue(i));
        //                        break;
        //                    case "decimal":
        //                        prop.SetValue(obj, rdr.IsDBNull(i) ? default(Decimal) : (Decimal)rdr.GetValue(i));
        //                        break;
        //                    case "single":
        //                        prop.SetValue(obj, rdr.IsDBNull(i) ? default(Single) : (Single)rdr.GetValue(i));
        //                        break;
        //                    case "double":
        //                        prop.SetValue(obj, rdr.IsDBNull(i) ? default(Double) : (Double)rdr.GetValue(i));
        //                        break;
        //                    case "boolean":
        //                        prop.SetValue(obj, rdr.IsDBNull(i) ? default(Boolean) : (Boolean)rdr.GetValue(i));
        //                        break;
        //                    case "byte":
        //                        prop.SetValue(obj, rdr.IsDBNull(i) ? default(Byte) : (Byte)rdr.GetValue(i));
        //                        break;
        //                    case "byte[]":
        //                        prop.SetValue(obj, rdr.IsDBNull(i) ? null : (Byte[])rdr.GetValue(i));
        //                        break;
        //                    case "datetime":
        //                        prop.SetValue(obj, rdr.IsDBNull(i) ? default(DateTime) : (DateTime)rdr.GetValue(i));
        //                        break;
        //                }
        //            }
        //        }

        //        if (prop.Name == "Version")
        //        {
        //            prop.SetValue(obj, version);
        //        }
        //    }
        //    return obj;
        //}

        //public static T MapSqlCommand<T>(this SqlCommand cmd)
        //{
        //    var obj = GetObject<T>();

        //    using (SqlDataReader reader = cmd.ExecuteReader())
        //    {
        //        while (reader.Read())
        //        {
        //            obj = obj.MapDataReader<T>(reader);
        //        }
        //    }

        //    return obj;
        //}

        //public static async Task<T> MapSqlCommandAsync<T>(this SqlCommand cmd)
        //{
        //    var obj = GetObject<T>();

        //    using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
        //    {
        //        while (await reader.ReadAsync())
        //        {
        //            obj = obj.MapDataReader<T>(reader);
        //        }
        //    }

        //    return obj;
        //}

        //public static List<T> MapSqlCommandList<T>(this SqlCommand cmd)
        //{
        //    var list = new List<T>();            

        //    using (SqlDataReader reader = cmd.ExecuteReader())
        //    {
        //        while (reader.Read())
        //        {                    
        //            var item = GetObject<T>();
        //            list.Add(item.MapDataReader<T>(reader));
        //        }
        //    }

        //    return list;
        //}

        //public static async Task<List<T>> MapSqlCommandListAsync<T>(this SqlCommand cmd)
        //{
        //    var list = new List<T>();

        //    using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
        //    {
        //        while (await reader.ReadAsync())
        //        {
        //            var item = GetObject<T>();
        //            list.Add(item.MapDataReader<T>(reader));
        //        }
        //    }

        //    return list;
        //}

        //public static List<T> MapSqlConnectionList<T>(this List<T> list, string connectionString, string sql, CommandType commandType)
        //{

        //    using (SqlConnection connection = new SqlConnection(connectionString))
        //    {
        //        using (SqlCommand command = new SqlCommand(sql, connection))
        //        {
        //            command.CommandType = commandType;
        //            connection.Open();
        //            using (SqlDataReader reader = command.ExecuteReader())
        //            {
        //                while (reader.Read())
        //                {
        //                    var item = GetObject<T>();
        //                    list.Add(item.MapDataReader<T>(reader));
        //                }
        //            }
        //        }
        //    }

        //    return list;
        //}

        //public async static Task<List<T>> MapSqlConnectionListAsync<T>(this List<T> list, string connectionString, string sql, CommandType commandType)
        //{

        //    using (SqlConnection connection = new SqlConnection(connectionString))
        //    {
        //        using (SqlCommand command = new SqlCommand(sql, connection))
        //        {
        //            command.CommandType = commandType;
        //            await connection.OpenAsync();
        //            using (SqlDataReader reader = await command.ExecuteReaderAsync())
        //            {
        //                while (await reader.ReadAsync())
        //                {
        //                    var item = GetObject<T>();
        //                    list.Add(item.MapDataReader<T>(reader));
        //                }
        //            }
        //        }
        //    }

        //    return list;
        //}

        //// https://www.c-sharpcorner.com/blogs/create-object-to-generic-or-unknown-class
        //private static T GetObject<T>(params object[] lstArgument)
        //{
        //    return (T)Activator.CreateInstance(typeof(T), lstArgument);
        //}


    }
}
