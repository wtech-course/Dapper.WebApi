using Dapper.Application.Interfaces;
using Dapper.Core.Entities;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static Dapper.SqlMapper;

namespace Dapper.Infrastructure.Repository
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        private readonly IConfiguration configuration;
        string _tableName;
        private IEnumerable<PropertyInfo> GetProperties => typeof(TEntity).GetProperties();
        public GenericRepository(IConfiguration configuration)
        {
            this.configuration = configuration;           
        }
        public async Task<int> AddAsync(TEntity entity)
        {
            try
            {
                _tableName = typeof(TEntity).Name;
                var insertQuery = GenerateInsertQuery_ScopeIdentity();

                using (var connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
                {
                    connection.Open();
                    var result = await connection.ExecuteAsync(insertQuery, entity);
                    return result;
                }
            }
            catch (Exception ex)
            {

                throw;
            }

        }
        private static List<string> GenerateListOfProperties(IEnumerable<PropertyInfo> listOfProperties)
        {
            return (from prop in listOfProperties

                    let attributes = prop.GetCustomAttributes(typeof(DescriptionAttribute), false)
                    where (attributes.Length <= 0 || (attributes[0] as DescriptionAttribute)?.Description != "Ignore") && prop.Name != "Id"
                    select prop.Name).ToList();
        }
        private string GenerateInsertQuery_ScopeIdentity()
        {
            var insertQuery = new StringBuilder($"INSERT INTO {_tableName} ");
            insertQuery.Append("(");
            var properties = GenerateListOfProperties(GetProperties);         
            properties.ForEach(prop => { insertQuery.Append($"[{prop}],"); });
            insertQuery
            .Remove(insertQuery.Length - 1, 1)
            .Append(") VALUES (");
            properties.ForEach(prop => { insertQuery.Append($"@{prop},"); });
            insertQuery
            .Remove(insertQuery.Length - 1, 1)
            .Append(")");
            insertQuery = new StringBuilder(string.Format("{0}; SELECT SCOPE_IDENTITY();", insertQuery, _tableName));
            return insertQuery.ToString();
        }

        public async Task<int> DeleteAsync(int id)
        {
            _tableName = typeof(TEntity).Name;
            var sql = "DELETE FROM " + _tableName + " WHERE Id = @Id";
            using (var connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var result = await connection.ExecuteAsync(sql, new { Id = id });
                return result;
            }
        }

        public async Task<IReadOnlyList<TEntity>> GetAllAsync()
        {
            _tableName = typeof(TEntity).Name;
            var sql = "SELECT * FROM " + _tableName + "";
            using (var connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var result = await connection.QueryAsync<TEntity>(sql);
                return result.ToList();
            }
        }

        public async Task<TEntity> GetByIdAsync(int id)
        {
            _tableName = typeof(TEntity).Name;
            var sql = "SELECT * FROM " + _tableName + "  WHERE Id = @Id";
            using (var connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var result = await connection.QuerySingleOrDefaultAsync<TEntity>(sql, new { Id = id });
                return result;
            }
        }

        public async Task<int> UpdateAsync(TEntity entity)
        {
            _tableName = typeof(TEntity).Name;
            var updateQuery = GenerateUpdateQuery();
         
            using (var connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var result = await connection.ExecuteAsync(updateQuery, entity);
                return result;
            }
        }
        private string GenerateUpdateQuery()
        {
            var updateQuery = new StringBuilder($"UPDATE {_tableName} SET ");
            var properties = GenerateListOfProperties(GetProperties);
            properties.ForEach(property =>
            {
                if (!property.Equals("id"))
                {
                    updateQuery.Append($"{property}=@{property},");
                }
            });
            updateQuery.Remove(updateQuery.Length - 1, 1);
            updateQuery.Append(" WHERE Id=@Id");
            return updateQuery.ToString();
        }

        public async Task<IReadOnlyList<TEntity>> GetAllJoinAsync(string query)
        {
            using (var connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();

                var result = connection.Query<ClassRoom, Lesson, ClassRoom>(query, (os, o) => { os.Id = o.ClassRoom_Id; return os; }, splitOn: "ClassRoom_Id,Lesson_Name");
                return (IReadOnlyList<TEntity>)result;
            }
        }
    }
}
