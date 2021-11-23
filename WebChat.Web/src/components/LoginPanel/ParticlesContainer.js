import Particles from 'react-tsparticles';

export const ParticlesContainer = () => {

    const particlesInit = (main) => {
        console.log(main);
    };

    const particlesLoaded = (container) => {
        console.log(container);
    };

    return (
        <Particles
            height="99vh"
            id="tsparticles"
            init={particlesInit}
            loaded={particlesLoaded}
            options={{
                fullScreen: false,
                particles: {
                    number: {
                      value: 0
                    },
                    collisions: {
                      enable: false
                    },
                    color: {
                      value: "#ffffff"
                    },
                    shape: {
                      type: "circle"
                    },
                    opacity: {
                      value: { min: 0.3, max: 0.8 },
                      animation: {
                        enable: true,
                        speed: 1,
                        minimumValue: 0.1,
                        sync: false,
                      }
                    },
                    size: {
                      value: { min: 1, max: 10 }
                    },
                    move: {
                      enable: true,
                      size: true,
                      speed: 5,
                      direction: "none",
                      outModes: {
                        default: "destroy"
                      },
                      trail: {
                        enable: true,
                        fillColor: "#030518",
                        length: 3
                      }
                    }
                  },
                  detectRetina: true,
                  emitters: {
                    direction: "none",
                    rate: {
                      delay: 0.25,
                      quantity: 10
                    },
                    position: {
                      x: 50,
                      y: 50
                    },
                    size: {
                      width: 0,
                      height: 0
                    },
                    spawnColor: {
                      value: "#ff0000",
                      animation: {
                        h: {
                          enable: true,
                          speed: 5
                        },
                        l: {
                          enable: true,
                          speed: 0,
                          offset: {
                            min: 20,
                            max: 80
                          }
                        }
                      }
                    }
                }
            }}
        />
    );
}

export default ParticlesContainer;