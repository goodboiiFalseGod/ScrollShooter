using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.UnityComponents
{
    public static class Pool
    {
        public static List<Bullet> bullets = new List<Bullet>();
        public static List<Asteroid> asteroids = new List<Asteroid>();
    }
}
