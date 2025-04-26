﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Exceptions
{
    public sealed class BasketNotFoundExecption(string id) : NotFoundException($"Basket With Id = {id} is not found")
    {
    }
}
