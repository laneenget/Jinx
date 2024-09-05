using System.Linq;
using Jinx.Src.Math;

namespace Jinx.Src.Lights {

    public class LightManager() {

        public List<Light> lights;
        public List<float> lightTypes;
        public List<float> lightPositions;
        public List<float> ambientIntensities;
        public List<float> diffuseIntensities;
        public List<float> specularIntensities;

        public LightManager() {

            this.lights = new List<Light>();
            this.lightTypes = new List<float>();
            this.lightPositions = new List<float>();
            this.ambientIntensities = new List<float>();
            this.diffuseIntensities = new List<float>();
            this.specularIntensities = new List<float>();
        }

        public void Clear() {

            this.lights.Clear();
            this.lightTypes.Clear();
            this.lightPositions.Clear();
            this.ambientIntensities.Clear();
            this.diffuseIntensities.Clear();
            this.specularIntensities.Clear();
        }

        public void AddLight(Light light) {

            bool alreadyAdded = this.lights.Any(elem => elem == light);
            
            if(!alreadyAdded) {
                this.lights.Add(light);
            }
        }
    }
}