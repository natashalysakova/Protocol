using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Protocols
{
    [Serializable]
    public struct Mechanic
    {
        private readonly int id;
        private readonly string name;
        private readonly string job;

        public Mechanic(int id, string name, string job)
        {
            this.id = id;
            this.name = name;
            this.job = job;
        }

        public int Id
        {
            get { return id; }
        }

        public string Name
        {
            get { return name; }
        }

        public string Job
        {
            get { return job; }
        }
    }
}
