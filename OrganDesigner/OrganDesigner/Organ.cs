using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using System.Collections;
/// <summary>
/// TODO:
/// CODE INITIALIZATION FUNCTIONS AND TEST
/// </summary>
namespace OrganDesigner
{
    public class Organ
    {
        public const int WriterI = 0;
        public const int CapacitorI = 1;
        public const int MotorI = 2;
        public const int StructureI = 3;
        public const int BetaI = 4;
        public const int PumpI = 5;
        public const int VisionI = 6;
        public const int lastChargeableOrgan = 2;
        //input static parameters
        public float startHealth, pC, metabolism, maxM;

        //between frame static
        public float coreM, dynamicM, tKe, psionLevel;

        //Calculated statistic
        public float healthiness, currentPower;

        //dynamic variables in between frames
        public float inQ, outQ, usedQ;
        public Vector3 inMTP, outMTP, toProcessMTP;

        public Structure parent;

        public Organ(float startHealth, float powerConsumption, float metabolism, float maxM)
        {
            coreM = startHealth;
            this.startHealth = startHealth;
            pC = powerConsumption;
            this.metabolism = metabolism;
            this.maxM = maxM;
            tKe = startHealth * EnergyManager.roomTemp;
        }
        public virtual void flowIn()
        {
            toProcessMTP = inMTP;
            outMTP = inMTP = Vector3.Zero;
        }
        public virtual void flowOut()
        {
            //how much actually is released
            Vector3 netMTP = outMTP * healthiness;

            float phononsReleased = 0;
            float psionsReleased = 0;

            if (coreM + dynamicM > 0)
            {
                phononsReleased = getTemperature() * netMTP.X;
            }
            if (coreM > 0)
            {
                psionsReleased = psionLevel / coreM * netMTP.X;
            }

            tKe -= phononsReleased;
            psionLevel -= psionsReleased;

            Vector3 remainingToFlow = toProcessMTP * healthiness;

            outMTP += new Vector3(0, phononsReleased, psionsReleased);

            parent.inMTP += netMTP + remainingToFlow;

            parent.stomachM += outMTP.X - netMTP.X + toProcessMTP.X - remainingToFlow.X;     //health inefficiencies leak into stomach
            parent.psionLevel += outMTP.Z - netMTP.Z + toProcessMTP.Z - remainingToFlow.Z;
            parent.addPhonons(outMTP.Y - netMTP.Y + toProcessMTP.Y - remainingToFlow.Y);
        }
        public virtual void absorb()
        {
            float minM = Math.Min(metabolism, toProcessMTP.X);
            Vector3 deltaMTP = Vector3.Zero;
            if (minM > 0)
            {

                deltaMTP = minM / toProcessMTP.X * toProcessMTP;
                psionLevel += deltaMTP.Z;
                tKe += deltaMTP.Y;
                toProcessMTP -= deltaMTP;
                deltaMTP = new Vector3(deltaMTP.X, 0, 0);
                float usedM = Math.Min(deltaMTP.X, startHealth - (coreM + dynamicM));//if health below normal, get minimum of health needed, available matter, and regeneration
                dynamicM += usedM;
                Vector3 incorporated = deltaMTP * usedM / deltaMTP.X;
                deltaMTP -= incorporated;

            }

            healthiness = (dynamicM + coreM) / startHealth;

            outMTP += deltaMTP;
        }
        public virtual void continuousIn()
        {
            usedQ = Math.Min(pC / healthiness, inQ);//get max of inQ and power necessary to meet powerconsumption demand after heating
            float netQ = usedQ * healthiness;//usable energy
            tKe += (usedQ - netQ);//ohmic heating (using power because the overall result would be described as the sum, or in calculus the integral.

            outQ = inQ - usedQ;//amount of energy left over
            currentPower = netQ / pC;
            inQ = 0;
        }
        public virtual void continuousOut()
        {

            parent.c.inQ += outQ;
            usedQ = 0;
        }
        public virtual void bruise(float core, float dynamic, bool chunk = false)
        {
            float dynamicDamage = Math.Min(dynamic, dynamicM);
            float leftOverDynamic = dynamic - dynamicDamage;
            float coreDamage = Math.Min(core + leftOverDynamic, coreM);
            float leftOverCore = core - coreDamage;//what is actually cascaded to structure
            float deltaPsion = coreM <= 0 ? 0 : coreDamage / coreM * psionLevel;
            float deltaPhonon = coreM + dynamicM <= 0 ? 0 : (dynamicDamage + coreDamage) / (coreM + dynamicM) * tKe;
            //        print((dynamicDamage + coreDamage) / (coreM + dynamicM) + " " + totalThermalEnergy);
            psionLevel -= deltaPsion;
            addPhonons(-deltaPhonon);
            if (!chunk)
            {
                parent.stomachM += dynamicDamage + coreDamage;//add organ bruising cascade
                parent.psionLevel += deltaPsion;
                parent.addPhonons(deltaPhonon);
            }
            else
            {
                //print(parent.body);
                //float deltaM = Mathf.Min(dynamicDamage + coreDamage, parent.body.transform.mass - .01f);
                //parent.realMass.mass = parent.body.transform.mass -= deltaM;
                //parent.body.updateMatter(-deltaM);            
            }
            dynamicM -= dynamicDamage;
            coreM -= coreDamage;
            healthiness = (coreM + dynamicM) / startHealth;
            if (leftOverCore > 0)
            {
                if (this == parent)
                {
                    parent.death("Total loss of functional organ");
                }
                else
                    parent.bruise(0, leftOverCore, chunk);
            }
            if (coreM + dynamicM == 0)
            {
                // Debug.LogError("ORGAN DEATH");
            }
        }
        public float getTemperature()
        {
            return tKe / (coreM + dynamicM);
        }
        public void addPhonons(float phonons)
        {
            tKe += phonons;
        }
    }
    public class Structure : Organ
    {
        //Static Parameter
        public float baseBps, fatGrowth, fatBreakdown, psionBloodHomeostasis;

        //Between frame resources
        public float stomachM, fatM, crystalPsion;

        //Between frame stats
        public float charge, curBps;

        //Organ Organization

        public Writer w;
        public Capacitor c;
        public Motor m;
        public Beta b;
        public Pump p;
        public Vision v;
        public Organ[] organs;

        bool stabilized;
        
        //Calculated Static Values
        float sD, cD, bD, pD, mD, vD, wD, totalDemand;

        //Initializes organ system
        public Structure(float[] startHealths, float[] metabolisms, float[] powerConsumptions, float[] maxMs, float[] maxCharges, int maxBoostCount, float betaRate, float drainRate, float fatGrowth, float fatBreakdown, float baseBps, float homeostasis) : base(startHealths[StructureI], powerConsumptions[StructureI], metabolisms[StructureI], maxMs[StructureI])
        {
            //Instantiate Organs
            c = new Capacitor(startHealths[CapacitorI], powerConsumptions[CapacitorI], metabolisms[CapacitorI], maxMs[CapacitorI], maxCharges[CapacitorI]);
            w = new Writer(startHealths[WriterI], powerConsumptions[WriterI], metabolisms[WriterI], maxMs[WriterI], maxCharges[WriterI]);
            m = new Motor(startHealths[MotorI], powerConsumptions[MotorI], metabolisms[MotorI], maxMs[MotorI], maxCharges[MotorI], maxBoostCount);
            b = new Beta(startHealths[BetaI], powerConsumptions[BetaI], metabolisms[BetaI], maxMs[BetaI], betaRate);
            p = new Pump(startHealths[PumpI], powerConsumptions[PumpI], metabolisms[PumpI], maxMs[PumpI], drainRate);
            v = new Vision(startHealths[VisionI], powerConsumptions[VisionI], metabolisms[VisionI], maxMs[VisionI]);

            organs = new Organ[] { w, c, m, this, b, p, v };
            c.parent = w.parent = m.parent = b.parent = p.parent = v.parent = parent = this;

            c.p = p;
            c.s = this;
            c.v = v;
            c.w = w;
            c.m = m;
            //Parameters
            this.fatBreakdown = fatBreakdown;
            this.fatGrowth = fatGrowth;
            this.baseBps = baseBps;
            this.psionBloodHomeostasis = homeostasis;
        }

        //Actually starts it
        public void startSimulation()
        {


            initPipeDemands();
            c.initPowerDemands();
        }
        public override void absorb()
        {
            float minM = Math.Min(metabolism, toProcessMTP.X);
            Vector3 deltaMTP = (minM == 0 ? 0 : minM / toProcessMTP.X) * toProcessMTP;
            float rawPsions = deltaMTP.Z;
            addPhonons(deltaMTP.Y);
            toProcessMTP -= deltaMTP;
            deltaMTP = new Vector3(deltaMTP.X, 0, 0);


            //Console.WriteLine(toProcessMTP + " " + deltaMTP.X);
            //continuous starvation - healing -> body uses inM to heal itself, finds that it doesn't have enough for everyone else, gives part of itself to everyone. This oscillates until equilibrium
            //If blood flow will be low regardless of how much blood is currently here
            if (Math.Round(toProcessMTP.X + outMTP.X + deltaMTP.X, 3) < totalDemand)//if blood flow is too low, add directly to output deposit from dynamicM
            {
                //Console.WriteLine("LESS THAN ENOUGH BY " + (totalDemand - Math.Round(toProcessMTP.X + outMTP.X + deltaMTP.X, 3)));
                //TODO: Do I need this?
              //  if (deltaMTP.X == 0)//if toProcessM is depleted but the stuff in outM isn't enough -> deltaM would be zero. If this isn't here, even if there is stuff ready to process, it will detect a defecit.  DO I NEED THIS?
                {

                    //print(totalDemand - (outM + toProcessM + deltaM) + " " + deltaM);
                    float delta = Math.Min(totalDemand - (outMTP.X + toProcessMTP.X + deltaMTP.X), metabolism);//get minimum of deficit and what can be offered via metabolism

                    if (dynamicM > delta)//if enough dynamic, pull from dynamic
                    {
                        dynamicM -= delta;
                        deltaMTP.X += delta;
                    }
                    else
                    {

                        if (coreM + dynamicM > delta)//starve
                        {
                            deltaMTP.X += delta;
                            float deltaHealth = delta - dynamicM;
                            psionLevel += crystalPsion * deltaHealth / coreM; //add to body psions from crystal proportional to percent of matter lost
                            crystalPsion *= 1 - deltaHealth / coreM;// subtract percent of psions lost due to matter loss
                            coreM -= deltaHealth;
                            dynamicM = 0;
                        }
                        else
                        {
                            death("Death by Starvation");
                            //death by starvation
                        }
                    }
                }

            }
            else//care about self after caring for other organs
            {
                //if (deltaMTP.X > 0)//if I have more than enough health left
                //    print(deltaMTP.X);
                //Seems like using this area of code destroys things
                //print(startHealth - (coreM + dynamicM));
                if (coreM + dynamicM > startHealth)//if more than enough total health, get fat using matter from deltaM.
                {
                    // if (Math.Round(toProcessM + outM + deltaM, 3) >= totalDemand)//if enough dynamic + core to get fat, and dynamic isn't needed for future healing or resupplying
                    // {
                    //if full health
                    float delta = Math.Min(fatGrowth, (coreM + dynamicM) - startHealth);
                    fatM += delta;//gain fat

                    //if (dynamicM < delta)
                    //    print("THIS SHOULDN't BE HAPPENING!");

                    dynamicM -= delta;
                    //Vector3 incorporated = deltaMTP * delta / deltaMTP.X;
                    //deltaMTP -= incorporated;

                    // }
                }
                //stomachM already pumped into dynamicM by default. Deducting anything from deltaM will cause a defecit in flowM, leading to decrease in overall flow statically. To heal dynamicM, can only use fatM for direct. Because of beta, flow will always return less than needed, forcing deltaM to always be zero here
                else //if less  than enough total health, use deltaM
                {
                    float usedM = Math.Min(Math.Min(fatM, fatBreakdown), startHealth - (coreM + dynamicM));//if health below normal, get minimum of health needed, available matter, and regeneration
                    dynamicM += usedM;
                    fatM -= usedM;
                }
            }

            //print(toProcessMTP.Z + outMTP.Z + " " + (outMTP.X + deltaMTP.X + toProcessMTP.X) + " rawpsions " + rawPsions);
            ///Psions are like fat -> fat doesn't go directly to any one place in the body on command, but when sugar levels are low fat is put there. There is a baseline sugar concentration expected.

            if (Math.Round(toProcessMTP.Z + outMTP.Z, 3) < psionBloodHomeostasis * (outMTP.X + deltaMTP.X + toProcessMTP.X))//Is current concentration acceptable?
            {//too low
             //print("TOO LOW " + rawPsions);
                float delta = psionBloodHomeostasis * (outMTP.X + deltaMTP.X + toProcessMTP.X) - (toProcessMTP.Z + outMTP.Z);
                if (delta > rawPsions)
                {
                    if (psionLevel + rawPsions > delta)
                    {
                        psionLevel -= delta - rawPsions;
                        outMTP.Z += delta;
                    }
                    else if (psionLevel + crystalPsion + rawPsions > delta)
                    {
                        crystalPsion -= delta - rawPsions - psionLevel;
                        psionLevel = 0;
                        outMTP.Z += delta;
                    }
                    else
                    {
                        outMTP.Z += crystalPsion + psionLevel + rawPsions;
                        crystalPsion = 0;
                        psionLevel = 0;
                    }
                    rawPsions = 0;
                }
                else
                {
                    outMTP.Z += delta;
                    rawPsions -= delta;
                    crystalPsion += rawPsions;
                }

            }
            float crystalCap = coreM * EnergyManager.psionPerKg;

            //Add whatever is left
            {//there is a surplus, so put it somewhere  -> Body has no way of decreasing blood psion level unless it is directly stored into glyphWriter manually, or leakage.
             //this part uses remaining rawpsions
             ///print("TOO HIGH " + rawPsions);
                if (crystalPsion < crystalCap)//if crystal not yet fill
                {
                    float delta = Math.Min(rawPsions, crystalCap - crystalPsion);
                    crystalPsion += delta;
                    rawPsions -= delta;
                    psionLevel += rawPsions;
                    rawPsions = 0;
                    //psionLevel += usedP - delta;//add whatever is left over to body
                }
                else
                {
                    psionLevel += rawPsions;
                    rawPsions = 0;
                }

            }


            //Console.WriteLine("LESS THAN ENOUGH BY " + (totalDemand - Math.Round(toProcessMTP.X + outMTP.X + deltaMTP.X, 3)));
            //Console.WriteLine(deltaMTP);
            outMTP += deltaMTP;

            healthiness = (dynamicM + coreM) / startHealth;

        }

        public void initPipeDemands()
        {
            totalDemand = c.maxM + b.maxM + p.maxM + m.maxM + v.maxM + w.maxM + maxM;
            sD = maxM / totalDemand;
            cD = c.maxM / totalDemand;
            bD = b.maxM / totalDemand;
            pD = p.maxM / totalDemand;
            mD = m.maxM / totalDemand;
            vD = v.maxM / totalDemand;
            wD = w.maxM / totalDemand;
        }
        float temperature
        {
            get
            {
                return tKe / (coreM + dynamicM + stomachM + fatM);
            }
        }

        float bps
        {
            get
            {

                float realBps = (float)Math.Round(baseBps * temperature / EnergyManager.roomTemp * currentPower * healthiness, 3);
                //Console.WriteLine(temperature + " " + currentPower + " " + healthiness);
                if (float.IsInfinity(realBps) || float.IsNaN(realBps))
                {
                    //throw new Exception("BPS IS NAN OR INFINITY " + temperature + " is temp " + currentPower + " is currentPower");
                    realBps = baseBps;
                    return baseBps;
                }
                if (!stabilized)
                {
                    if (realBps <= 0)
                    {
                        return 1;
                    }
                    else
                    {
                        stabilized = true;
                        return realBps;
                    }
                }
                else
                {
                    if (realBps <= 0)
                    {
                        if (tKe == 0)
                            death("Death by Hypothermia");
                        else if (currentPower == 0)
                            death("Death by Energy Deficiency");
                        else
                            death("Death by Cardiac Damage");
                        return 1;
                    }
                    else
                    {
                        return realBps;
                    }
                }

            }
        }
        /// <summary>
        /// </summary>
        public override void flowOut()
        {
            //float netMatter = outM;// * healthiness;
            //stomachM += outM - netMatter;

            // float netPsion = inP;// * healthiness;
            //psionLevel += netPsion;
            //body.psionLevel += inP - netPsion;

            //reason to not have psionDemand: Since distribution is already happening via matter diffusion on matter mtp exchange, it is unnecessary and also unnatural. This would give double edged blade effect for increasing blood flow

            float phononsReleased = getTemperature() * outMTP.X;
            float psionsReleased = psionLevel / coreM * outMTP.X;
            addPhonons(-phononsReleased);
            psionLevel -= psionsReleased;
            //print(psionsReleased);
            outMTP += new Vector3(0, phononsReleased, psionsReleased) + toProcessMTP;

            c.inMTP = outMTP * cD;
            w.inMTP = outMTP * wD;
            m.inMTP = outMTP * mD;
            v.inMTP = outMTP * vD;
            p.inMTP = outMTP * pD;
            b.inMTP = outMTP * bD;
            inMTP += outMTP * sD;
        }
        public void death(string cause)
        {

        }
        internal IEnumerable<float> Discrete()
        {
            yield return 0;
            while (true)
            {
                //            print(body.temperature);


                float time = 0;
                curBps = bps;
                //v.time = (1 / curBps) / .05f;

                flowIn();
                v.flowIn();
                b.flowIn();
                c.flowIn();
                w.flowIn();
                m.flowIn();
                p.flowIn();

                if (time >= 1 / curBps)
                {
                    //print(curBps + " WHAT");
                    yield return 0;
                }
                else
                {
                    while (time < 1 / curBps)
                    {
                        continuousIn();

                        v.continuousIn();
                        b.continuousIn();
                        c.continuousIn();
                        w.continuousIn();
                        m.continuousIn();
                        p.continuousIn();

                        v.absorb();
                        b.absorb();
                        c.absorb();
                        w.absorb();
                        m.absorb();
                        p.absorb();
                        absorb();

                        v.continuousOut();
                        b.continuousOut();
                        c.continuousOut();
                        w.continuousOut();
                        m.continuousOut();
                        p.continuousOut();
                        continuousOut();
                        time += 0.05f;

                        yield return .05f;
                    }

                    v.flowOut();
                    b.flowOut();
                    c.flowOut();
                    w.flowOut();
                    m.flowOut();
                    p.flowOut();

                    flowOut();
                }
                yield return .01f;
            }
        }
    }
    public class Beta : Organ
    {
        //input static params
        public float betaRate;

        public Beta(float startHealth, float powerConsumption, float metabolism, float maxM, float betaRate) : base(startHealth, powerConsumption, metabolism, maxM)
        {
            this.betaRate = betaRate;
        }
        public override void continuousIn()
        {
        }
        public override void absorb()
        {
            float minM = Math.Min(metabolism, toProcessMTP.X);
            Vector3 deltaMTP = Vector3.Zero;
            if (minM > 0)
            {
                deltaMTP = minM / toProcessMTP.X * toProcessMTP;
                psionLevel += deltaMTP.Z;
                tKe += deltaMTP.Y;
                toProcessMTP -= deltaMTP;
                deltaMTP = new Vector3(deltaMTP.X, 0, 0);
            }
            //TODO: Make this based on separate beta level resource
            bruise(0, startHealth * contaminationRate);//damaged with contamination (at end because this is covered up by inM in flowing

            if (minM > 0)
            {
                float usedM = Math.Min(deltaMTP.X, startHealth - (coreM + dynamicM));//if health below normal, get minimum of health needed, available matter, and regeneration
                dynamicM += usedM;
                Vector3 incorporated = deltaMTP * usedM / deltaMTP.X;
                deltaMTP -= incorporated;


            }
            healthiness = (dynamicM + coreM) / startHealth;

            outMTP += deltaMTP;
        }
        public override void continuousOut()
        {
            float realFlow = betaRate;
            parent.charge += -realFlow * (1 - healthiness);//leak charge into body
            parent.c.inQ += realFlow * healthiness;
        }
        float contaminationRate
        {
            get
            {
                return (float)Math.Exp(-16 / betaRate) / 4;
            }
        }

    }

    public class Pump : Organ
    {
        //input static parameters
        public float maxDrainRate;


        public Pump( float startHealth, float powerConsumption, float metabolism, float maxM, float maxDrainRate) : base(startHealth, powerConsumption, metabolism, maxM)
        {
            this.maxDrainRate = maxDrainRate;
        }

        public override void absorb()
        {
            float minM = Math.Min(metabolism, toProcessMTP.X);
            Vector3 deltaMTP = Vector3.Zero;
            if (minM > 0)
            {

                deltaMTP = minM / toProcessMTP.X * toProcessMTP;
                psionLevel += deltaMTP.Z;
                tKe += deltaMTP.Y;

                toProcessMTP -= deltaMTP;
                deltaMTP = new Vector3(deltaMTP.X, 0, 0);
                //now look to get more health maybe
                float usedM = Math.Min(Math.Min(deltaMTP.X, startHealth - (coreM + dynamicM)), 0);//if health below normal, get minimum of health needed, available matter, and regeneration
                dynamicM += usedM;
                Vector3 incorporated = deltaMTP * usedM / deltaMTP.X;
                deltaMTP -= incorporated;

            }
            healthiness = (dynamicM + coreM) / startHealth;


            float toCvt = drainAmt * currentPower;
            parent.stomachM -= toCvt;
            parent.dynamicM += toCvt;

            //psion pump unique, splits how much toCvt is sent to output, how much is leaked into organ psion level
            float realP = toCvt * EnergyManager.psionPerKg;
            outMTP.Z += realP * healthiness;
            psionLevel += realP * (1 - healthiness);

            outMTP += deltaMTP;
        }

        public float drainAmt
        {
            get
            {
                return Math.Min(parent.stomachM, maxDrainRate);//get minimum of what is in there and what can be taken
            }

        }
    }

    public class Vision : Organ
    {

        public Vision(float startHealth, float powerConsumption, float metabolism, float maxM) : base(startHealth, powerConsumption, metabolism, maxM)
        {

        }

    }

    public class Capacitor : ChargeableOrgan
    {
        public Pump p;
        public Structure s;//this is just parent
        public Vision v;
        public Writer w;
        public Motor m;

        float sD, pD, mD, vD, wD, totalDemand;

        public Capacitor(float startHealth, float powerConsumption, float metabolism, float maxM, float maxCharge) : base(startHealth, powerConsumption, metabolism, maxM, maxCharge)
        {
        }

        public void initPowerDemands()
        {
            s = parent;

            totalDemand = p.pC + m.pC + v.pC + w.pC + s.pC;
            sD = pC / totalDemand;
            pD = p.pC / totalDemand;
            mD = m.pC / totalDemand;
            vD = v.pC / totalDemand;
            wD = w.pC / totalDemand;
        }
        public override void continuousIn()
        {
            charge += inQ;
            charge = Math.Min(maxCharge, charge);
            inQ = 0;
        }
        public override void continuousOut()
        {
            float rawQ;
            // if (charge <= maxQ)// if leq charge than maxQ, flow the smallest of what is needed and what there is
            {
                rawQ = Math.Min(totalDemand / healthiness, charge);
            }
            /*  else// if more charge than maxQ, have over + demand
              {
                  rawQ = charge - maxQ + totalDemand;
              }*/
            charge -= rawQ;
            //parent.body.temperature += (rawEnergy * (1 - healthiness))/parent.body.transform.mass;

            float netQ = rawQ * healthiness;
            outQ = netQ;
            if (coreM + dynamicM > 0)
                tKe += rawQ - netQ;//ohmic heating (using power because the overall result would be described as the sum, or in calculus the integral.

            s.inQ += netQ * sD;
            v.inQ += netQ * vD;
            m.inQ += netQ * mD;
            w.inQ += netQ * wD;
            p.inQ += netQ * pD;

        }
    }

    public class Writer : ChargeableOrgan
    {
        //between frame resource
        public float crystalPsions;

        //used for internal realtime psion charging control
        bool usePsions;

        public Writer(float startHealth, float powerConsumption, float metabolism, float maxM, float maxCharge) : base(startHealth, powerConsumption, metabolism, maxM, maxCharge)
        {
        }
        public override void bruise(float dynamic, float core, bool chunk = false)// assume charge is stored per unit mass in dynamic. Implement dynamic charge-based vaporization ease
        {

            float initCore = coreM;
            base.bruise(core, dynamic, chunk);
            float deltaPsion = coreM / initCore * crystalPsions;
            crystalPsions -= deltaPsion;
            if (!chunk)
                psionLevel += deltaPsion;
        }
        public override void continuousIn()
        {
            outQ = inQ - usedQ;
            currentPower = usedQ / pC;
            inQ = 0;
        }
        public override void absorb()
        {
            float minM = Math.Min(metabolism, toProcessMTP.X);
            Vector3 deltaMTP = Vector3.Zero;
            float rawPsions = 0;
            if (minM > 0)
            {
                deltaMTP = minM / toProcessMTP.X * toProcessMTP;
                rawPsions = deltaMTP.Z;
                tKe += deltaMTP.Y;
                toProcessMTP -= deltaMTP;
                deltaMTP = new Vector3(deltaMTP.X, 0, 0);

                float usedM = Math.Min(deltaMTP.X, startHealth - (coreM + dynamicM));//if health below normal, get minimum of health needed, available matter, and regeneration
                dynamicM += usedM;
                Vector3 incorporated = deltaMTP * usedM / deltaMTP.X;
                deltaMTP -= incorporated;
            }

            if (usePsions)
            {
                crystalPsions += rawPsions;
                rawPsions = 0;
                usePsions = false;
            }

            healthiness = (dynamicM + coreM) / startHealth;

            psionLevel += rawPsions;
            outMTP += deltaMTP;
        }
        public void tapPsion()
        {
            usePsions = true;
        }
        public void tapCharge(float flowDegree = 1)
        {
            float inQUsed = inQ * flowDegree;
            float netQ = inQUsed * healthiness;

            tKe += (inQUsed - netQ);//ohmic heating (using power because the overall result would be described as the sum, or in calculus the integral.
            charge += netQ;
            usedQ = inQUsed;//input all, output some      
        }
    }


    public class Motor : ChargeableOrgan
    {
        //Static parameter
        public int maxBoostCount;


        //Calculated internal variable
        float chargePerBoost;
        public Motor(float startHealth, float powerConsumption, float metabolism, float maxM, float maxCharge, int maxBoostCount ) : base(startHealth, powerConsumption, metabolism, maxM, maxCharge)
        {
            this.maxBoostCount = maxBoostCount;
            chargePerBoost = maxCharge / maxBoostCount;
        }
        public override void continuousIn()
        {

            outQ = inQ - usedQ;
            if (outQ > 0)
            {
                if (charge < maxCharge)
                {
                    float deltaQ = Math.Min(maxCharge - charge, outQ);
                    outQ -= deltaQ;
                    charge += deltaQ;
                    usedQ += deltaQ;
                }
            }
            currentPower = usedQ / pC;
            inQ = 0;
        }
        internal float getEnergyMultiplier()//called in update, before cycle
        {
            float netQ = inQ * healthiness;
            tKe += (inQ - netQ);//ohmic heating (using power because the overall result would be described as the sum, or in calculus the integral.
            usedQ = inQ;
            return netQ * EnergyManager.speedPerCharge;
        }
        internal float getBoost()
        {
            if (charge > chargePerBoost)
            {

                float netQ = chargePerBoost * healthiness;
                tKe += (chargePerBoost - netQ);//ohmic heating (using power because the overall result would be described as the sum, or in calculus the integral.
                charge -= chargePerBoost;
                return netQ * EnergyManager.speedPerCharge;

            }
            else
            {
                return 0;
            }
        }
    }
    public class ChargeableOrgan : Organ
    {
        //static parameter
        public float maxCharge;

        internal float charge;

        public ChargeableOrgan(float startHealth, float powerConsumption, float metabolism, float maxM, float maxCharge) : base(startHealth, powerConsumption, metabolism, maxM)
        {
            this.maxCharge = maxCharge;
        }
        public override void bruise(float dynamic, float core, bool chunk = false)// assume charge is stored per unit mass in dynamic. Implement dynamic charge-based vaporization ease
        {
            if (dynamic > startHealth)
            {
                parent.charge -= charge;
                charge = 0;
            }
            else
            {
                parent.charge -= charge * dynamic / startHealth;
                charge -= charge * dynamic / startHealth;
            }
            base.bruise(dynamic, core, chunk);
        }
    }

  
    public class EnergyManager
    {
        public const float roomTemp = 30;
        internal static float psionPerKg = 1;//.5f
        internal const float speedPerCharge = 16;//.3f

    }


}
