using CalculateMathExpression.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculateMathExpression.Utils
{

    class ObjectLocator
    {
        Dictionary<string, object> _classObjectHolder;
        private ObjectLocator()
        {
            _classObjectHolder = new Dictionary<string, object>();
        }
        private static class InstanceHolder
        {
            public static ObjectLocator Instance = new ObjectLocator();
        }
        public static ObjectLocator Current()
        {
            return InstanceHolder.Instance;
        }
        public void RegisterInstance<T>(T instance)
        {
            if (_classObjectHolder.Count == 0)
            {
                _classObjectHolder.Add(typeof(T).Name, instance);
            } else
            {
                
                bool isExist = false;
               
                foreach (string type in _classObjectHolder.Keys)
                {
                    if (type.Equals(typeof(T).Name))
                    {
                        _classObjectHolder.Remove(typeof(T).Name);
                        _classObjectHolder.Add(typeof(T).Name, instance);
                        isExist = true;
                        break;
                    }

                }
                if (!isExist)
                {
                    _classObjectHolder.Add(typeof(T).Name, instance);
                }
            }
        }
        public T GetInstance<T>()
        {
            object ob = null;
            if (_classObjectHolder.TryGetValue(typeof(T).Name, out ob))
            {
                return (T) ob;
            }
            else
            {
                throw new InstanceNotFoundException("Can't not found any instance of " + typeof(T).Name);
            }


        }
    }
}
