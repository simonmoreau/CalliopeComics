using McMaster.Extensions.CommandLineUtils;
using MediatR;
using Application.Series.Queries.SearchSeriesQuery;
using System;
using System.Threading.Tasks;

namespace CLI
{
    [Command(Name = "serie", Description = "Serie related commands")]
    [Subcommand(typeof(List), typeof(Show))]
    internal class SeriesCmd
    {
        protected int OnExecute(CommandLineApplication app)
        {
            app.ShowHelp();
            return 0;
        }

        [Command(Name = "list", Description = "List series")]
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
                var series = await _mediator.Send(new SearchSeriesQuery(Query));
                foreach (var s in series)
                {
                    _console.WriteLine($"{s.Id}: {s.Name}");
                }
                return 0;
            }
        }

        [Command(Name = "show", Description = "Show serie details")]
        internal class Show
        {
            [Option(CommandOptionType.SingleValue, ShortName = "id", LongName = "serie-id", Description = "The Serie Id", ShowInHelpText = true)]
            public int SerieId { get; set; }

            private readonly IMediator _mediator;
            private readonly IConsole _console;

            public Show(IMediator mediator, IConsole console)
            {
                _mediator = mediator;
                _console = console;
            }

            protected async Task<int> OnExecute()
            {
                // Placeholder, as I don't know the exact query for showing a single serie
                _console.WriteLine($"Show serie {SerieId}");
                return 0;
            }
        }
    }
}
