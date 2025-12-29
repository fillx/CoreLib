using System.Collections.Generic;
using System.Threading;

namespace CoreLib.AssetLoader
{
    internal class CancellationManager
    {
        private readonly List<CancellationTokenSource> _sources = new();

        public CancellationToken CreateToken() {
            var source = new CancellationTokenSource();
            _sources.Add(source);
            return source.Token;
        }

        public CancellationTokenSource CreateTokenSource() {
            var source = new CancellationTokenSource();
            _sources.Add(source);
            return source;
        }

        public void Track(CancellationTokenSource source) {
            if (source != null && !_sources.Contains(source)) {
                _sources.Add(source);
            }
        }

        public void CancelAll() {
            foreach (var source in _sources) {
                if (!source.IsCancellationRequested) {
                    source.Cancel();
                }
            }
            _sources.Clear();
        }

        public void Remove(CancellationTokenSource source) {
            _sources.Remove(source);
        }

        public void ClearCompleted() {
            _sources.RemoveAll(s => s.IsCancellationRequested);
        }
    }
}