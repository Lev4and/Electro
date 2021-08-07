using Electro.Model.Database;
using System.Threading;
using System.Threading.Tasks;

namespace Electro.Model.Importers
{
    public class BaseImporter
    {
        private CancellationTokenSource _cancellationTokenSource;
        private CancellationToken _cancellationToken;

        private protected readonly DataManager _dataManager;

        public ImportStatus Status { get; private protected set; }

        public ImportTimer Timer { get; private protected set; }

        public ImportProgress Progress { get; private protected set; }

        public BaseImporter(DataManager dataManager)
        {
            _cancellationTokenSource = new CancellationTokenSource();
            _cancellationToken = _cancellationTokenSource.Token;

            _dataManager = dataManager;

            Status = ImportStatus.NotStarted;

            Timer = new ImportTimer();
            Progress = new ImportProgress();
        }

        public bool IsRunning()
        {
            return Status != ImportStatus.NotStarted && Status != ImportStatus.Stopped && Status != ImportStatus.Completed;
        }

        public async Task Start()
        {
            //Timer.Start();

            Status = ImportStatus.Started;

            await Import(_cancellationToken);
        }

        public void Stop()
        {
            //Timer.Stop();
            //Progress.Reset();

            Status = ImportStatus.Stopped;

            _cancellationTokenSource.Cancel();
        }

        private protected virtual async Task Import(CancellationToken cancellationToken)
        {

        }
    }
}
