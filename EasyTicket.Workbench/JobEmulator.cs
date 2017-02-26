using System.Threading.Tasks;
using ProcessRequestJob;

namespace EasyTicket.Workbench {
    public class JobEmulator {
        private readonly TicketChecker TicketChecker;

        public JobEmulator() {
            TicketChecker = new TicketChecker();
        }

        public async void Run() {
            while (true) {
                TicketChecker.Check();
                await Task.Delay(100000);
            }
        }
    }
}