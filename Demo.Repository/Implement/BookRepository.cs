using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using Dapper;
using Demo.Repository.Interface;
using Demo.Repository.Models;

namespace Demo.Repository.Implement
{
    public class BookRepository : IBookRepository
    {
        /// <summary>
        /// 連線字串
        /// </summary>
        //private readonly string _connectString = @"Server=(localdb)\MSSQLLocalDB;Database=DemoContext-801384d6-ba4d-43a9-887c-1c41fae63236;Trusted_Connection=True;MultipleActiveResultSets=true";
        private readonly string _connectString;

        public BookRepository(string connectString)
        {
            this._connectString = connectString;
        }

        /// <summary>
        /// 查詢資料清單
        /// </summary>
        /// <returns></returns>
        public IEnumerable<BookDataModel> GetList(BookSearchCondition condition)
        {
            var sql = "SELECT * FROM Book ";

            var sqlQuery = new List<string>();
            var parameter = new DynamicParameters();

            if (string.IsNullOrWhiteSpace(condition.Name) is false)
            {
                sqlQuery.Add($" Name LIKE @Name ");
                parameter.Add("Name", $"%{condition.Name}%");
            }

            if (string.IsNullOrWhiteSpace(condition.Title) is false)
            {
                sqlQuery.Add($" Title == @Title ");
                parameter.Add("Title", $"%{condition.Title}%");
            }

            if (string.IsNullOrWhiteSpace(condition.Genre) is false)
            {
                sqlQuery.Add($" Genre == @Genre ");
                parameter.Add("Genre", $"%{condition.Genre}%");
            }

            if (sqlQuery.Any())
            {
                sql += $" WHERE {string.Join(" AND ", sqlQuery)} ";
            }

            using (var conn = new SqlConnection(_connectString))
            {
                var result = conn.Query<BookDataModel>(sql, parameter);
                return result;
            }
        }

        /// <summary>
        /// 查詢單筆資料
        /// </summary>
        /// <param name="id">Book Id</param>
        /// <returns></returns>   
        public BookDataModel Get(int id)
        {
            var sql =
            @"		
                SELECT * 
                FROM Book 
                Where Id = @id
            ";

            var parameters = new DynamicParameters();
            parameters.Add("Id", id, System.Data.DbType.Int32);

            using (var conn = new SqlConnection(_connectString))
            {
                var result = conn.QueryFirstOrDefault<BookDataModel>(sql, parameters);
                return result;
            }
        }

        /// <summary>
        /// 新增單筆資料
        /// </summary>
        /// <param name="parameter">單筆資料</param>
        /// <returns></returns>
        public bool Insert(BookCondition condition)
        {
            var sql =
            @"
                INSERT INTO Book 
                (
                   [Name]
                  ,[Title]
                  ,[Genre]
                  ,[ReleaseDate]
                  ,[Price]
                ) 
                VALUES 
                (
                    @Name
                   ,@Title
                   ,@Genre
                   ,@ReleaseDate
                   ,@Price
                );

                SELECT @@IDENTITY;
            ";

            using (var conn = new SqlConnection(_connectString))
            {
                var result = conn.Execute(sql, condition);
                return result > 0;
            }
        }

        /// <summary>
        /// 更新單筆資料
        /// </summary>
        /// <param name="id">Book Id</param>
        /// <param name="parameter">單筆資料</param>
        /// <returns></returns>
        public bool Update(int id, BookCondition condition)
        {
            var sql =
            @"
                UPDATE Book
                SET 
                    [Name] = @Name
                   ,[Title] = @Title
                   ,[Genre] = @Genre
                   ,[ReleaseDate] = @ReleaseDate
                   ,[Price] = @Price
                WHERE
                    Id = @id
            ";

            var parameters = new DynamicParameters();

            parameters.AddDynamicParams(condition);
            parameters.Add("Id", id, System.Data.DbType.Int32);

            using (var conn = new SqlConnection(_connectString))
            {
                var result = conn.Execute(sql, parameters);
                return result > 0;
            }
        }

        /// <summary>
        /// 刪除單筆資料
        /// </summary>
        /// <param name="id">Book Id</param>
        /// <returns></returns>
        public bool Delete(int id)
        {
            var sql =
            @"
                DELETE FROM Book
                WHERE Id = @id
            ";

            var parameters = new DynamicParameters();
            parameters.Add("Id", id, System.Data.DbType.Int32);

            using (var conn = new SqlConnection(_connectString))
            {
                var result = conn.Execute(sql, parameters);
                return result > 0;
            }
        }
    }
}
