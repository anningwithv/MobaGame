using System;
using System.Collections.Generic;
using System.Text;

namespace HOKServer {
    public class Singleton<T> where T : new() {
        private static T instance;
        public static T Instance {
            get {
                if(instance == null) {
                    instance = new T();
                }
                return instance;
            }
        }

        public virtual void Init() { }

        public virtual void Update() { }
    }
}
