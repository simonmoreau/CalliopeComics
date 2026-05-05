using McMaster.Extensions.CommandLineUtils;
using MediatR;
using Application.Issues.Queries.SearchIssuesQuery;
using Application.Issues.Queries.GetIssueDetailsQuery;
using Application.Services.ComicService;
using Domain.Entities;
using System;
using System.Threading;
using System.Threading.Tasks;
using Domain.DTO;

namespace CLI
{
    [Command(Name = "issue", Description = "Issue related commands")]
    [Subcommand(typeof(List), typeof(Show), typeof(Write))]
    internal class IssueCmd
    {
        protected int OnExecute(CommandLineApplication app)
        {
            app.ShowHelp();
            return 0;
        }

        [Command(Name = "list", Description = "List issues")]
        internal class List
        {
            [Option(CommandOptionType.SingleValue, ShortName = "q", LongName = "query", Description = "Search query", ShowInHelpText = true)]
            public string Query { get; set; }

            private readonly IMediator _mediator;
            private readonly IConsole _console;

            public List(IMediator mediator, IConsole console)
            {
                _mediator = mediator;
                _console = console;
            }

            protected async Task<int> OnExecute()
            {
                List<IssueDto> issues = await _mediator.Send(new SearchIssuesQuery(Query));
                foreach (IssueDto i in issues)
                {
                    _console.WriteLine($"{i.Id}: {i.Title}");
                }
                return 0;
            }
        }

        [Command(Name = "show", Description = "Show issue details")]
        internal class Show
        {
            [Option(CommandOptionType.SingleValue, ShortName = "id", LongName = "issue-id", Description = "The Issue Id", ShowInHelpText = true)]
            public int IssueId { get; set; }

            private readonly IMediator _mediator;
            private readonly IConsole _console;

            public Show(IMediator mediator, IConsole console)
            {
                _mediator = mediator;
                _console = console;
            }

            protected async Task<int> OnExecute()
            {
                GcdIssue issue = await _mediator.Send(new GetIssueDetailsQuery(IssueId));
                _console.WriteLine($"{issue.Id}: {issue.Title}");
                return 0;
            }
        }

        [Command(Name = "write", Description = "Write issue details to path")]
        internal class Write
        {
            [Option(CommandOptionType.SingleValue, ShortName = "id", LongName = "issue-id", Description = "The Issue Id", ShowInHelpText = true)]
            public int IssueId { get; set; }

            [Option(CommandOptionType.SingleValue, ShortName = "p", LongName = "path", Description = "The path to the comic archive", ShowInHelpText = true)]
            public string Path { get; set; }

            private readonly IMediator _mediator;
            private readonly IComicService _comicService;

            public Write(IMediator mediator, IComicService comicService)
            {
                _mediator = mediator;
                _comicService = comicService;
            }

            protected async Task<int> OnExecute()
            {
                GcdIssue issue = await _mediator.Send(new GetIssueDetailsQuery(IssueId));
                ComicInfo comicInfo = _comicService.CreateComicInfo(issue);
                await _comicService.SaveComicInfo(comicInfo, Path);
                return 0;
            }
        }
    }
}
