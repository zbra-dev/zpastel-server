﻿using System.Collections.Generic;
using System.Threading.Tasks;
using ZPastel.Model;

namespace ZPastel.Service.Repositories
{
    public interface IPastelRepository
    {
        Task<IReadOnlyList<Pastel>> FindAll();
    }
}