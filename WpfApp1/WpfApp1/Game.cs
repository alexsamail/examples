using System;
using System.Collections;
using System.Collections.Generic;


namespace Enumerables
{
    public class GameCatalog
    {
        public string Name { get; set; }
        public String Image { get; set; }
        public int Number { get; set; }
        public string Genre { get; set; }
        public string Description { get; set; }
        public void Item(string name, string image, int number, string genre, string desc)
        {
            this.Name = name;
            this.Image = image;
            this.Number = number;
            this.Genre = genre;
            this.Description = desc;
        }
        public string GetName()
        {
            return this.Name;
        }
    }
    public class GameEnumerable : IEnumerable<GameCatalog>
    {
        public GameCatalog[] gameCatalog = new GameCatalog[0];
        public int ItemsCount
        {
            get { return gameCatalog.Length; }
        }
        public void AddItem(string name, string image, int number, string genre, string desc)
        {
            Array.Resize(ref gameCatalog, ItemsCount + 1);
            gameCatalog[ItemsCount - 1] = new GameCatalog();
            gameCatalog[ItemsCount - 1].Item(name, image, number, genre, desc);
        }
        public void ChangeItem(string name, string image, int number, string genre, string desc)
        {
            int index = SearchItem(name);
            gameCatalog[index].Item(name, image, number, genre, desc);
        }
        public int SearchItem(string name)
        {
            for (int i = 0; i < gameCatalog.Length; i++)
            {
                if (gameCatalog[i].GetName() == name) return i;
            }
            return -1;
        }
        public void RemoveItem(string name)
        {
            int index = this.SearchItem(name);
            GameCatalog[] array = new GameCatalog[gameCatalog.Length - 1];
            for (int i = 0, j = 0; i < array.Length; i++, j++)
            {
                if (index == j) j++;
                array[i] = new GameCatalog();
                array[i] = gameCatalog[j];
            }
            Array.Resize(ref gameCatalog, gameCatalog.Length - 1);
            Array.Copy(array, gameCatalog, gameCatalog.Length);
            Array.Resize(ref array, 0);
        }

        private class GameEnumerator : IEnumerator<GameCatalog>
        {
            private readonly GameCatalog[] _data;
            private int _position = -1;
            public GameEnumerator(GameCatalog[] values)
            {
                _data = new GameCatalog[values.Length];
                Array.Copy(values, _data, values.Length);
            }
            public GameCatalog Current
            {
                get { return _data[_position]; }
            }
            object IEnumerator.Current => Current;

            public bool MoveNext()
            {
                if (_position < _data.Length - 1)
                {
                    _position++;
                    return true;
                }
                return false;
            }
            public void Reset()
            {
                _position = -1;
            }
            public void Dispose() { }
        }
        public IEnumerator<GameCatalog> GetEnumerator() => new GameEnumerator(gameCatalog);
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
