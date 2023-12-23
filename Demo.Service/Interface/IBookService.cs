using System;
using System.Collections.Generic;
using System.Text;
using Demo.Service.Models;

namespace Demo.Service.Interface
{
    public interface IBookService
    {
        /// <summary>
        /// 查詢資料清單
        /// </summary>
        /// <returns></returns>
        IEnumerable<BookResultModel> GetList(BookSearchInfo info);

        /// <summary>
        /// 查詢單筆資料
        /// </summary>
        /// <param name="id">Book Id</param>
        /// <returns></returns>   
        BookResultModel Get(int id);

        /// <summary>
        /// 新增單筆資料
        /// </summary>
        /// <param name="parameter">單筆資料</param>
        /// <returns></returns>
        bool Insert(BookInfo info);

        /// <summary>
        /// 更新單筆資料
        /// </summary>
        /// <param name="id">Book Id</param>
        /// <param name="parameter">單筆資料</param>
        /// <returns></returns>
        bool Update(int id, BookInfo info);

        /// <summary>
        /// 刪除單筆資料
        /// </summary>
        /// <param name="id">Book Id</param>
        /// <returns></returns>
        bool Delete(int id);
    }
}
