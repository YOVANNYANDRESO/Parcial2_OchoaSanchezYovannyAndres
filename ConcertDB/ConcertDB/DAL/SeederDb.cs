using ConcertDB.DAL.Entities;

namespace ConcertDB.DAL
{
    public class SeederDb
    {
        private readonly DatabaseContext _context;

        public SeederDb(DatabaseContext context)
        {
            _context = context;
        }

        public  async Task SeederAsync()
        {
            await _context.Database.EnsureCreatedAsync();//remplaza el update-database
            await PopulateTicketssAsync();
        }

        private async Task PopulateTicketssAsync()
        {
            if(!_context.Ticketss.Any())
            {
                _context.Ticketss.Add(new Ticket { });
            }
        }
    }
}
