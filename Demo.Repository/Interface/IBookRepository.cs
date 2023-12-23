using System;
using System.Collections.Generic;
using System.Text;
using Demo.Repository.Models;

namespace Demo.Repository.Interface
{
    public interface IBookRepository
    {
        /// <summary>
        /// 查詢資料清單
        /// </summary>
        /// <returns></returns>
        IEnumerable<BookDataModel> GetList(BookSearchCondition info);

        /// <summary>
        /// 查詢單筆資料
        /// </summary>
        /// <param name="id">Book Id</param>
        /// <returns></returns>   
        BookDataModel Get(int id);

        /// <summary>
        /// 新增單筆資料
        /// </summary>
        /// <param name="parameter">單筆資料</param>
        /// <returns></returns>
        bool Insert(BookCondition info);

        /// <summary>
        /// 更新單筆資料
        /// </summary>
        /// <param name="id">Book Id</param>
        /// <param name="parameter">單筆資料</param>
        /// <returns></returns>
        bool Update(int id, BookCondition info);

        /// <summary>
        /// 刪除單筆資料
        /// </summary>
        /// <param name="id">Book Id</param>
        /// <returns></returns>
        bool Delete(int id);
    }
}
