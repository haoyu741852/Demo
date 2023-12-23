using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using Demo.Repository.Implement;
using Demo.Repository.Interface;
using Demo.Repository.Models;
using Demo.Service.Interface;
using Demo.Service.Mappings;
using Demo.Service.Models;

namespace Demo.Service.Implement
{
    public class BookService : IBookService
    {
        private readonly IMapper _mapper;
        private readonly IBookRepository _bookRepository;

        public BookService(IBookRepository bookRepository)
        {
            var config = new MapperConfiguration(cfg => cfg.AddProfile<ServiceMappings>());

            this._mapper = config.CreateMapper();
            this._bookRepository = bookRepository;
        }

        /// <summary>
        /// 查詢資料清單
        /// </summary>
        /// <returns></returns>
        public IEnumerable<BookResultModel> GetList(BookSearchInfo info)
        {
            var condition = this._mapper.Map<BookSearchInfo, BookSearchCondition>(info);
            var data = this._bookRepository.GetList(condition);
            var result = this._mapper.Map<IEnumerable<BookDataModel>, IEnumerable<BookResultModel>>(data);
            return result;
        }

        /// <summary>
        /// 查詢單筆資料
        /// </summary>
        /// <param name="id">Book Id</param>
        /// <returns></returns>   
        public BookResultModel Get(int id)
        {
            var data = this._bookRepository.Get(id);
            var result = this._mapper.Map<BookDataModel, BookResultModel>(data);
            return result;
        }

        /// <summary>
        /// 新增單筆資料
        /// </summary>
        /// <param name="parameter">單筆資料</param>
        /// <returns></returns>
        public bool Insert(BookInfo info)
        {
            var condition = this._mapper.Map<BookInfo, BookCondition>(info);
            var result = this._bookRepository.Insert(condition);
            return result;
        }

        /// <summary>
        /// 更新單筆資料
        /// </summary>
        /// <param name="id">Book Id</param>
        /// <param name="parameter">單筆資料</param>
        /// <returns></returns>
        public bool Update(int id, BookInfo info)
        {
            var condition = this._mapper.Map<BookInfo, BookCondition>(info);
            var result = this._bookRepository.Update(id, condition);
            return result;
        }

        /// <summary>
        /// 刪除單筆資料
        /// </summary>
        /// <param name="id">Book Id</param>
        /// <returns></returns>
        public bool Delete(int id)
        {
            var result = this._bookRepository.Delete(id);
            return result;
        }
    }
}
