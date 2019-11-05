using DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.Interfaces
{
   public interface IunitofWork
    {
        GenericRepositori<History> HistoryRepo { get; }
    }
}
