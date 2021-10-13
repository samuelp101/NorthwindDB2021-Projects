using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Threading.Tasks;

namespace NorthwindDB2021.Data.ORM
{
  public static class SqlServerOrm
  {
    public static T MapSqlDataReader<T>(this T obj, SqlDataReader rdr, string version = "none")
    {
      var props = GetProps<T>(obj);

      string OrderByValue = String.Empty;

      foreach (var prop in props)
      {
        for (int i = 0; i < rdr.FieldCount; i++)
        {
          if (string.Compare(rdr.GetName(i).Replace("_", ""), prop.Name, ignoreCase: true) == 0)
          {
            string propType = String.Empty;

            if (prop.PropertyType.Name == "Nullable`1")
            {
              propType = Nullable.GetUnderlyingType(prop.PropertyType).Name;
            }
            else
            {
              propType = prop.PropertyType.Name;
            }

            switch (propType.ToLower())
            {
              case "string":
                prop.SetValue(obj, rdr.GetValue(i).ToString());
                break;
              case "int16":
                prop.SetValue(obj, rdr.IsDBNull(i) ? default(Int16) : (Int16)rdr.GetValue(i));
                break;
              case "int32":
                prop.SetValue(obj, rdr.IsDBNull(i) ? default(Int32) : (Int32)rdr.GetValue(i));
                break;
              case "int64":
                prop.SetValue(obj, rdr.IsDBNull(i) ? default(Int64) : (Int64)rdr.GetValue(i));
                break;
              case "decimal":
                prop.SetValue(obj, rdr.IsDBNull(i) ? default(Decimal) : (Decimal)rdr.GetValue(i));
                break;
              case "single":
                prop.SetValue(obj, rdr.IsDBNull(i) ? default(Single) : (Single)rdr.GetValue(i));
                break;
              case "double":
                prop.SetValue(obj, rdr.IsDBNull(i) ? default(Double) : (Double)rdr.GetValue(i));
                break;
              case "boolean":
                prop.SetValue(obj, rdr.IsDBNull(i) ? default(Boolean) : (Boolean)rdr.GetValue(i));
                break;
              case "byte":
                prop.SetValue(obj, rdr.IsDBNull(i) ? default(Byte) : (Byte)rdr.GetValue(i));
                break;
              case "byte[]":
                prop.SetValue(obj, rdr.IsDBNull(i) ? null : (Byte[])rdr.GetValue(i));
                break;
              case "datetime":
                prop.SetValue(obj, rdr.IsDBNull(i) ? default(DateTime) : (DateTime)rdr.GetValue(i));
                break;
            }
          }
        }

        if (prop.Name == "Version")
        {
          prop.SetValue(obj, version);
        }
      }
      return obj;
    }

    public static T MapSqlCommand<T>(this SqlCommand cmd)
    {
      var obj = GetObject<T>();

      using (SqlDataReader reader = cmd.ExecuteReader())
      {
        while (reader.Read())
        {
          obj = obj.MapSqlDataReader<T>(reader);
        }
      }
      return obj;
    }

    public static async Task<T> MapSqlCommandAsync<T>(this SqlCommand cmd)
    {
      var obj = GetObject<T>();

      using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
      {
        while (await reader.ReadAsync())
        {
          obj = obj.MapSqlDataReader<T>(reader);
        }
      }
      return obj;
    }

    public static List<T> MapSqlQueryList<T>(this List<T> list, string connectionString, string sql, CommandType commandType)
    {
      using (SqlConnection connection = new SqlConnection(connectionString))
      {
        using (SqlCommand command = new SqlCommand(sql, connection))
        {
          command.CommandType = commandType;
          connection.Open();
          using (SqlDataReader reader = command.ExecuteReader())
          {
            while (reader.Read())
            {
              T item = GetObject<T>();
              list.Add(item.MapSqlDataReader<T>(reader));
            }
          }
        }
      }
      return list;
    }

    public async static Task<List<T>> MapSqlQueryListAsync<T>(this List<T> list, string connectionString, string sql, CommandType commandType)
    {
      using (SqlConnection connection = new SqlConnection(connectionString))
      {
        using (SqlCommand command = new SqlCommand(sql, connection))
        {
          command.CommandType = commandType;
          await connection.OpenAsync();
          using (SqlDataReader reader = await command.ExecuteReaderAsync())
          {
            while (await reader.ReadAsync())
            {
              T item = GetObject<T>();
              list.Add(item.MapSqlDataReader<T>(reader));
            }
          }
        }
      }
      return list;
    }

    public static T MapSqlQuery<T>(this T obj, string connectionString, string sql, CommandType commandType)
    {
      using (SqlConnection connection = new SqlConnection(connectionString))
      {
        using (SqlCommand command = new SqlCommand(sql, connection))
        {
          command.CommandType = commandType;

          var props = GetProps<T>(obj);
          foreach (var prop in props)
          {
            command.Parameters.AddWithValue(prop.Name, prop.GetValue(obj));
          }
          connection.Open();
          obj = command.MapSqlCommand<T>();

        }
      }
      return obj;
    }

    public async static Task<T> MapSqlQueryAsync<T>(this T obj, string connectionString, string sql, CommandType commandType)
    {
      using (SqlConnection connection = new SqlConnection(connectionString))
      {
        using (SqlCommand command = new SqlCommand(sql, connection))
        {
          command.CommandType = commandType;

          var props = GetProps<T>(obj);
          foreach (var prop in props)
          {
            command.Parameters.AddWithValue(prop.Name, prop.GetValue(obj));
          }
          await connection.OpenAsync();
          obj = await command.MapSqlCommandAsync<T>();

        }
      }
      return obj;
    }

    public static int MapSqlQuery<T>(this T obj, string connectionString, string sql)
    {
      int result = 0;
      using (SqlConnection connection = new SqlConnection(connectionString))
      {
        using (SqlCommand command = new SqlCommand(sql, connection))
        {
          command.CommandType = CommandType.StoredProcedure;

          var props = GetProps<T>(obj);
          foreach (var prop in props)
          {
            command.Parameters.AddWithValue(prop.Name, prop.GetValue(obj));
          }
          connection.Open();
          result = command.ExecuteNonQuery();
        }
      }
      return result;
    }

    public async static Task<int> MapSqlQueryAsync<T>(this T obj, string connectionString, string sql)
    {
      int result = 0;
      using (SqlConnection connection = new SqlConnection(connectionString))
      {
        using (SqlCommand command = new SqlCommand(sql, connection))
        {
          command.CommandType = CommandType.StoredProcedure;

          var props = GetProps<T>(obj);
          foreach (var prop in props)
          {
            command.Parameters.AddWithValue(prop.Name, prop.GetValue(obj));
          }
          await connection.OpenAsync();
          result = await command.ExecuteNonQueryAsync();
        }
      }
      return result;
    }

    private static T GetObject<T>(params object[] lstArgument)
    {
      return (T)Activator.CreateInstance(typeof(T), lstArgument);
    }

    private static PropertyInfo[] GetProps<T>(T obj)
    {
      return obj.GetType().GetProperties();
    }

  }
}
