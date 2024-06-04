using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kurs.Core.ADT
{
    internal struct VecStorage<T>
    {        
        /* default-initialize */
        public VecStorage()
        {
            _block = null;
            _capacity = 0;
        }

        /* destroy trait impl */
        public void destroy()
        {
            /* delete _block; */
            _block = null;
            _capacity = 0;
        }

        public void ensure_capacity(ulong cap)
        {
            alloc_impl(cap);
        }

        public T[] data()
        {
            return _block;
        }

        public ulong capacity()
        {
            return _capacity;
        }

        /*** detail ***/
        private T[] _block;
        private ulong _capacity;

        private T[] alloc_impl(ulong count)
        {
            T[] new_block = new T[count];
            if (_block != null)
            {
                Array.Copy(_block, new_block, (int)Math.Min(_capacity, count)); /* огромное спасибо, что размер требуется в i32 */
                /* delete _block; */
            }

            _block = new_block;
            _capacity = count;
            return new_block;
        }
    }

    class Vec<T>
    {
        /* default-initialize */
        public Vec()
        {
            /* ensure no unexpected NREs */
            _storage.ensure_capacity(16);
            _len = 0;
        }

        public void push_back(T value)
        {
            if (_len + 1u >= _storage.capacity())
            {
                _storage.ensure_capacity(_storage.capacity() + _storage.capacity() / 2 + 1);
            }

            _storage.data()[_len++] = value;
        }

        public IEnumerator<T> GetEnumerator()
        {
            if (_len == 0)
            {
                yield break;
            }

            T[] block = _storage.data(); // ref
            for (ulong i = 0; i < _len; i++)
            {
                yield return block[i];
            }
        }

        public ulong size()
        {
            return _len;
        }


        /*** detail ***/
        private VecStorage<T> _storage;
        private ulong _len;
    }
}
