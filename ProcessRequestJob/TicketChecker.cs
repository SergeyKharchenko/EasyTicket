using System;
using System.Threading.Tasks;
using EasyTicket.SharedResources;
using EasyTicket.SharedResources.Models;

namespace ProcessRequestJob {
    public class TicketChecker : IDisposable {
        private readonly UzDbContext _dbContext;
        private readonly UzClient _uzClient;

        public TicketChecker() {
            _dbContext = new UzDbContext();
            _uzClient = new UzClient();
        }

        public async void Check() {
            UzContext uzContext = await _uzClient.GetUZContext();
            foreach (Request request in _dbContext.Requests) {
                CheckTicket(uzContext, request);
            }
        }

        private void CheckTicket(UzContext uzContext, Request request) {
            //_uzClient.GetTrains(request.)
            //request.
        }

        public void Dispose() {
            _dbContext.Dispose();
        }
    }
}