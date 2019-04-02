using System;
using System.Collections; 
using System.Collections.Generic;

using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1
{
    public class GameCatalog
    {
        public string Name { get; set; }
        public String Image { get; set; }
        public int Number { get; set; }
        public string Genre { get; set; }
        public string Description { get; set; }
        public void Item(string _name, string _image, int _num, string _gen, string _desc)
        {
            this.Name = _name;
            this.Image = _image;
            this.Number = _num;
            this.Genre = _gen;
            this.Description = _desc;
        }
        public void Show()
        {
            Console.WriteLine($"{this.Name}:{this.Number}");
        }
        public string GetName()
        {
            return this.Name;
        }
    }
    public class GameEnumerable : IEnumerable<GameCatalog>
    {
        public GameCatalog[] array = new GameCatalog[0];
        public int ItemsCount
        {
            get { return array.Length; }
        }
        public void AddItem(string _name, string _image, int _num, string _gen, string _desc)
        {
            Array.Resize(ref array, ItemsCount + 1);
            array[ItemsCount - 1] = new GameCatalog();
            array[ItemsCount - 1].Item(_name, _image, _num, _gen, _desc);
        }
        public void ChangeItem( string _name, string _image, int _num, string _gen, string _desc)
        {
            int index = SearchItem(_name);
            array[index].Item(_name, _image, _num, _gen, _desc); 
        }
        public void GetItem(int index) => array[index].Show();
        public int  SearchItem(string Name)
        {
            for (int i = 0; i < array.Length; i++)
            {
                if (array[i].GetName() == Name) return i;
            }
            return -1;
        }
        public void RemoveItem(string name)
        {
            int index = this.SearchItem(name);
            GameCatalog[] arrStr = new GameCatalog[array.Length - 1];
            for (int i = 0, f = 0; i < arrStr.Length; i++, f++)
            {
                if (index == f) f++;
                arrStr[i] = new GameCatalog();
                arrStr[i] = array[f];
            }
            Array.Resize(ref array, array.Length - 1);
            Array.Copy(arrStr, array, array.Length);
            Array.Resize(ref arrStr, 0);
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
        public IEnumerator<GameCatalog> GetEnumerator() => new GameEnumerator(array);
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
