using System.Collections.Concurrent;

namespace WebApp
{
    class AccountCache : IAccountCache
    {
        private readonly ConcurrentDictionary<long, Account> _itemsById = new ConcurrentDictionary<long, Account>();
        private readonly ConcurrentDictionary<string, Account> _itemsByGuid = new ConcurrentDictionary<string, Account>();

        public bool TryGetValue(long accountId, out Account item)
        {
            return _itemsById.TryGetValue(accountId, out item);
        }

        public bool TryGetValue(string externalId, out Account item)
        {
            return _itemsByGuid.TryGetValue(externalId, out item);
        }

        public void AddOrUpdate(Account account)
        {
            _itemsById.AddOrUpdate(account.InternalId, account, (key, item) => account);
            _itemsByGuid.AddOrUpdate(account.ExternalId, account, (key, item) => account);
        }

        public bool TryRemove(long accountId, out Account account)
        {
            return _itemsById.TryRemove(accountId, out account);
        }

        public bool TryRemove(string externalId, out Account account)
        {
            return _itemsByGuid.TryRemove(externalId, out account);
        }

        public void Clear()
        {
            _itemsById.Clear();
            _itemsByGuid.Clear();
        }
    }
}