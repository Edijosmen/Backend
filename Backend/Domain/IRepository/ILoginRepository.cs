﻿using Backend.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Domain.IRepository
{
    public interface ILoginRepository
    {
        Task<Usuario> ValidateUser(string usuario, string password);
    }
}
