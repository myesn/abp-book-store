﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Xunit;
using Shouldly;
using Volo.Abp.Validation;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;

namespace Acme.BookStore.Samples
{
    public class BookAppService_Tests : BookStoreApplicationTestBase
    {
        private readonly IBookAppService _bookAppService;

        public BookAppService_Tests()
        {
            _bookAppService =  ServiceProvider.GetRequiredService<IBookAppService>();
        }

        [Fact]
        public async Task Should_Get_List_Of_Books()
        {
            // Act
            var result = await _bookAppService.GetListAsync(new PagedAndSortedResultRequestDto());

            // Assert
            result.TotalCount.ShouldBeGreaterThan(0);
            result.Items.ShouldContain(x => x.Name == "Test book 1");
        }

        [Fact]
        public async Task Should_Create_A_Valid_Book()
        {
            // Act
            var result = await _bookAppService.CreateAsync(new CreateUpdateBookDto
            {
                Name = "New test book 42",
                Price = 10,
                PublishDate = DateTime.Now,
                Type = BookType.ScienceFiction
            });

            // Assert
            result.Id.ShouldNotBe(Guid.Empty);
            result.Name.ShouldBe("New test book 42");
        }

        [Fact]
        public async Task Should_Not_Create_A_Book_Without_Name()
        {
            var exception = await Assert.ThrowsAsync<AbpValidationException>(async () =>
            {
                await _bookAppService.CreateAsync(new CreateUpdateBookDto
                {
                    Name = "",
                    Price = 10,
                    PublishDate = DateTime.Now,
                    Type = BookType.ScienceFiction
                });
            });

            exception.ValidationErrors
                .ShouldContain(x => x.MemberNames.Any(member => member == "Name"));
        }

    }
}
