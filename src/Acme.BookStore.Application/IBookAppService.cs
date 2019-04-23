﻿using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Acme.BookStore
{
    public interface IBookAppService:
        IAsyncCrudAppService<
            BookDto,
            Guid,
            PagedAndSortedResultRequestDto,
            CreateUpdateBookDto,
            CreateUpdateBookDto
            >
    {
    }
}
