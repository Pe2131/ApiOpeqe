using DAL;
using DAL.Models;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository
{
    public class UnitOfWork : IunitofWork, IDisposable
    {
        private readonly ApplicationDbContext _dbContext;
        public UnitOfWork(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        private GenericRepositori<History> _HistoryRepo;
        public GenericRepositori<History> HistoryRepo
        {
            get
            {
                if (_HistoryRepo == null)
                {
                    _HistoryRepo = new GenericRepositori<History>(_dbContext);
                }
                return _HistoryRepo;
            }
        }
        public void Dispose()
        {
            _dbContext.Dispose();
        }
    }
}
