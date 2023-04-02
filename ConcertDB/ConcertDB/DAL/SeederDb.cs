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
                for(int i = 1;1<=100;i++) { 
                _context.Ticketss.Add(new Ticket { UseDate=null, IsUsed=false, EntranceGate=null });
                   
                }
                await _context.SaveChangesAsync();
            }
        }
    }
}
