namespace EPSFramework.DAL.Core
{
    using System;
    using System.Data.Common;

    public sealed class TransactionKeeper : IDisposable, ITransactionHandler
    {
        /// <summary>
        /// The _accesor
        /// </summary>
        private IDataBaseProvider _accesor;

        /// <summary>
        /// Initializes a new instance of the <see cref="TransactionKeeper"/> class.
        /// </summary>
        /// <param name="accesor">The accesor.</param>
        public TransactionKeeper(IDataBaseProvider accesor)
        {
            this._accesor = accesor;
        }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="TransactionKeeper"/> is disposed.
        /// </summary>
        /// <value>
        ///   <c>true</c> if disposed; otherwise, <c>false</c>.
        /// </value>
        public bool Disposed { get; private set; }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Commits this instance.
        /// </summary>
        /// <exception cref="System.InvalidOperationException">The transaction in the dbProvider is not initialized or in a 'disabled state'</exception>
        public void Commit()
        {
            if (this._accesor.Tran == null || this._accesor.Tran.Connection == null)
            {
                throw new InvalidOperationException("The transaction in the dbProvider is not initialized or in a 'disabled state'");
            }

            this._accesor.Tran.Commit();
        }

        /// <summary>
        /// Rollbacks this instance.
        /// </summary>
        /// <exception cref="System.InvalidOperationException">The transaction in the dbProvider is not initialized or in a 'disabled state'</exception>
        public void Rollback()
        {
            if (this._accesor.Tran == null || this._accesor.Tran.Connection == null)
            {
                throw new InvalidOperationException("The transaction in the dbProvider is not initialized or in a 'disabled state'");
            }

            this._accesor.Tran.Rollback();
        }

        /// <summary>
        /// Begins the transactional mode. Renews the instance if it was disposed and start is resources again.
        /// </summary>
        /// <param name="conn">The connection.</param>
        /// <returns></returns>
        internal TransactionKeeper BeginTransactionalMode(DbConnection conn)
        {
            if (this.Disposed)
            {
                this.Disposed = false;
            }

            if (this._accesor.Conn == null)
            {
                this._accesor.Conn = conn; // The real transaction object is initialized during the first query.
            }

            this._accesor.InATransaction = true;

            return this;
        }

        /// <summary>
        /// Disposes the specified current instance indicating if is a continuous action or after a previous dispose.
        /// </summary>
        /// <param name="disposing">if set to <c>true</c> the operation is [disposing].</param>
        private void Dispose(Boolean disposing)
        {
            if (Disposed)
            {
                return;
            }

            if (disposing)
            {
                if (_accesor.Conn != null && _accesor.Conn.State == System.Data.ConnectionState.Open)
                {
                    _accesor.Conn.Dispose();
                    _accesor.Conn = null;
                }

                if (_accesor.Cmd != null)
                {
                    _accesor.Cmd.Dispose();
                    _accesor.Cmd = null;
                }
                

                if (_accesor.Tran != null)
                {
                    _accesor.Tran.Dispose();
                    _accesor.Tran = null;
                }

                _accesor.InATransaction = false;

                this.Disposed = true;
            }
        }
    }
}