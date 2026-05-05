using Application.Issues.Queries.GetIssueDetailsQuery;
using Application.Services.ComicService;
using Domain.Entities;
using McMaster.Extensions.CommandLineUtils;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace CLI
{
    [Command(Name = "ComicInfo", Description = "Write information about the comic")]
    class ComicInfoCmd
    {
        [Option(CommandOptionType.SingleValue, ShortName = "id", LongName = "id", Description = "The Grand Comic Database Issue Id", ValueName = "Issue Id", ShowInHelpText = true)]
        public int IssueId { get; set; }

        [Option(CommandOptionType.SingleValue, ShortName = "p", LongName = "path", Description = "The path to the comic archive", ValueName = "Path", ShowInHelpText = true)]
        public string Path { get; set; }

        private readonly IConsole _console;
        private readonly IMediator _mediator;
        private readonly IComicService _comicService;

        public ComicInfoCmd(IConsole console, IMediator mediator, IComicService comicService)
        {
            _console = console;
            _mediator = mediator;
            _comicService = comicService;
        }

        protected async Task<int> OnExecute(CommandLineApplication app)
        {
            try
            {
                CancellationToken stoppingToken = new CancellationToken();
                GetIssueDetailsQuery detailsQuery = new GetIssueDetailsQuery(IssueId);
                GcdIssue issue = await _mediator.Send(detailsQuery, stoppingToken);
                ComicInfo comicInfo = _comicService.CreateComicInfo(issue);

                _comicService.SaveComicInfo(comicInfo, Path);
                return 0;
            }
            catch (Exception ex)
            {
                return 1;
            }
        }
    }
}
