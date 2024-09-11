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

        public float GetNumLights() {

            return this.lights.Count;
        }

        public void UpdateLights() {

            foreach (Light light in this.lights) {

                var (worldPosition, worldRotation, worldScale) = light.WorldMatrix.Decompose();

                lightPositions.Add(worldPosition.X);
                lightPositions.Add(worldPosition.Y);
                lightPositions.Add(worldPosition.Z);

                lightTypes.Add(light.GetType());

                if (light.Visible) {
                    ambientIntensities.Add(light.AmbientIntensity.X);
                    ambientIntensities.Add(light.AmbientIntensity.Y);
                    ambientIntensities.Add(light.AmbientIntensity.Z);

                    diffuseIntensities.Add(light.DiffuseIntensity.X);
                    diffuseIntensities.Add(light.DiffuseIntensity.Y);
                    diffuseIntensities.Add(light.DiffuseIntensity.Z);

                    specularIntensities.Add(light.SpecularIntensity.X);
                    specularIntensities.Add(light.SpecularIntensity.Y);
                    specularIntensities.Add(light.SpecularIntensity.Z);
                } else {
                    ambientIntensities.Add(0);
                    ambientIntensities.Add(0);
                    ambientIntensities.Add(0);

                    diffuseIntensities.Add(0);
                    diffuseIntensities.Add(0);
                    diffuseIntensities.Add(0);

                    specularIntensities.Add(0);
                    specularIntensities.Add(0);
                    specularIntensities.Add(0);
                }
            }
        }
    }
}