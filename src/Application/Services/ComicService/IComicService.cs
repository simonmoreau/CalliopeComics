using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Services.ComicService
{
    public interface IComicService
    {
        ComicInfo CreateComicInfo(GcdIssue issue);
    }
}
