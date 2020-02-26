using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace OrganDesigner
{
    class FlatOrganSystem
    {
        public const int wI = 0;
        public const int cI = 1;
        public const int mI = 2;
        public const int sI = 3;
        public const int bI = 4;
        public const int pI = 5;
        public const int vI = 6;
        public const int lastChargeableOrgan = 2;
        //input static parameters
        public float[] startHealth, pC, metabolism, maxM;

        //between frame static
        public float[] coreM, dynamicM, tKe, psionLevel;

        //Calculated statistic
        public float[] healthiness, currentPower;

        //dynamic variables in between frames
        public float[] inQ, outQ, usedQ;
        public Vector3[] inMTP, outMTP, toProcessMTP;


        //Simulate write psion charging
        bool usePsions;

        //Static Parameter
        public float baseBps, fatGrowth, fatBreakdown, psionBloodHomeostasis;

        //Between frame resources
        public float stomachM, fatM, crystalPsions, crystalPsionw;

        //Between frame stats
        public float[] charge;

        public float bodyCharge;

        public float curBps;

        //Organ Organization
        public float[] maxCharge;
        float maxBoostCount, drain, beta;

        bool stabilized;

        //Calculated Static Values
        float sDP, cDP, bDP, pDP, mDP, vDP, wDP, totalDemandP;
        float sDE, pDE, mDE, vDE, wDE, totalDemandE;

        //Initializes organ system
        public FlatOrganSystem(float[] startHealths, float[] metabolisms, float[] powerConsumptions, float[] maxMs, float[] maxCharges, int maxBoostCount, float betaRate, float drainRate, float fatGrowth, float fatBreakdown, float baseBps, float homeostasis)
        {
            maxM = maxMs;
            startHealth = startHealths;
            pC = powerConsumptions;
            metabolism = metabolisms;
            this.maxBoostCount = maxBoostCount;
            drain = drainRate;
            beta = betaRate;
            dynamicM = new float[startHealth.Length];
            tKe = new float[startHealth.Length];
            psionLevel = new float[startHealth.Length];
            healthiness = new float[startHealth.Length];
            currentPower = new float[startHealth.Length];
            inQ = new float[startHealth.Length];
            outQ = new float[startHealth.Length];
            usedQ = new float[startHealth.Length];
            inMTP = new Vector3[startHealth.Length];
            outMTP = new Vector3[startHealth.Length];
            toProcessMTP = new Vector3[startHealth.Length];
            coreM = new float[startHealths.Length];
            for (int i = 0; i < startHealths.Length; i++)
            {
                coreM[i] = startHealths[i];
                tKe[i] = EnergyManager.roomTemp * coreM[i];
            }
            charge = new float[maxCharges.Length];
            maxCharge = maxCharges;
            //Parameters
            this.fatBreakdown = fatBreakdown;
            this.fatGrowth = fatGrowth;
            this.baseBps = baseBps;
            this.psionBloodHomeostasis = homeostasis;
        }


        public void absorb()
        {
            float minM = Math.Min(metabolism[sI], toProcessMTP[sI].X);
            Vector3 deltaMTP = Vector3.Zero;
            float rawPsions = 0;
            float finalTemp = getTemperature(sI);
            if (minM > 0)
            {
                deltaMTP = (minM / toProcessMTP[sI].X) * toProcessMTP[sI];
                rawPsions = deltaMTP.Z;
                finalTemp = (tKe[sI] + deltaMTP.Y) / (coreM[sI] + dynamicM[sI] + deltaMTP.X);
                toProcessMTP[sI] -= deltaMTP;
                deltaMTP = new Vector3(deltaMTP.X, 0, 0);
            }
            if (Math.Round(toProcessMTP[sI].X + outMTP[sI].X + deltaMTP.X, 3) < totalDemandP)//if blood flow is too low, add directly to output deposit from dynamicM[sI]
            {
                float delta = Math.Min(totalDemandP - (outMTP[sI].X + toProcessMTP[sI].X + deltaMTP.X), metabolism[sI]);//get minimum of deficit and what can be offered via metabolism[sI]
                if (dynamicM[sI] > delta)//if enough dynamic, pull from dynamic
                {
                    dynamicM[sI] -= delta;
                    deltaMTP.X += delta;
                }
                else
                {
                    if (coreM[sI] + dynamicM[sI] > delta)//starve
                    {
                        deltaMTP.X += delta;
                        float deltaHealth = delta - dynamicM[sI];
                        psionLevel[sI] += crystalPsions * deltaHealth / coreM[sI]; //add to body psions from crystal proportional to percent of matter lost
                        crystalPsions *= 1 - deltaHealth / coreM[sI];// subtract percent of psions lost due to matter loss
                        coreM[sI] -= deltaHealth;
                        dynamicM[sI] = 0;
                    }
                    else
                    {
                        Console.WriteLine("Death by Starvation");
                    }
                }
            }
            else//care about self after caring for other organs
            {
                if (coreM[sI] + dynamicM[sI] > startHealth[sI])//if more than enough total health, get fat using matter from deltaM.
                {
                    float delta = Math.Min(fatGrowth, (coreM[sI] + dynamicM[sI]) - startHealth[sI]);
                    fatM += delta;//gain fat
                    dynamicM[sI] -= delta;
                }
                else //if less  than enough total health, use deltaM
                {
                    float usedM = Math.Min(Math.Min(fatM, fatBreakdown), startHealth[sI] - (coreM[sI] + dynamicM[sI]));//if health below normal, get minimum of health needed, available matter, and regeneration
                    dynamicM[sI] += usedM;
                    fatM -= usedM;
                }
            }
            if (Math.Round(toProcessMTP[sI].Z + outMTP[sI].Z, 3) < psionBloodHomeostasis * (outMTP[sI].X + deltaMTP.X + toProcessMTP[sI].X))//Is current concentration acceptable?
            {//too low
                float delta = psionBloodHomeostasis * (outMTP[sI].X + deltaMTP.X + toProcessMTP[sI].X) - (toProcessMTP[sI].Z + outMTP[sI].Z);
                if (delta > rawPsions)
                {
                    if (psionLevel[sI] + rawPsions > delta)
                    {
                        psionLevel[sI] -= delta - rawPsions;
                        outMTP[sI].Z += delta;
                    }
                    else if (psionLevel[sI] + crystalPsions + rawPsions > delta)
                    {
                        crystalPsions -= delta - rawPsions - psionLevel[sI];
                        psionLevel[sI] = 0;
                        outMTP[sI].Z += delta;
                    }
                    else
                    {
                        outMTP[sI].Z += crystalPsions + psionLevel[sI] + rawPsions;
                        crystalPsions = 0;
                        psionLevel[sI] = 0;
                    }
                    rawPsions = 0;
                }
                else
                {
                    outMTP[sI].Z += delta;
                    rawPsions -= delta;
                    crystalPsions += rawPsions;
                }
            }
            float crystalCap = coreM[sI] * EnergyManager.psionPerKg;

            if (crystalPsions < crystalCap)//if crystal not yet fill
            {
                float delta = Math.Min(rawPsions, crystalCap - crystalPsions);
                crystalPsions += delta;
                rawPsions -= delta;
                psionLevel[sI] += rawPsions;
                rawPsions = 0;
            }
            else
            {
                psionLevel[sI] += rawPsions;
                rawPsions = 0;
            }
            tKe[sI] = finalTemp * (coreM[sI] + dynamicM[sI]);
            deltaMTP.Y = deltaMTP.X * finalTemp;
            outMTP[sI] += deltaMTP;
            healthiness[sI] = (dynamicM[sI] + coreM[sI]) / startHealth[sI];

        }

        public virtual void bruise(float core, float dynamic, int idx, bool chunk = false)
        {
            float dynamicDamage = Math.Min(dynamic, dynamicM[idx]);
            float leftOverDynamic = dynamic - dynamicDamage;
            float coreDamage = Math.Min(core + leftOverDynamic, coreM[idx]);
            float leftOverCore = core - coreDamage;//what is actually cascaded to structure
            float deltaPsion = coreM[idx] <= 0 ? 0 : coreDamage / coreM[idx] * psionLevel[idx];
            float deltaPhonon = coreM[idx] + dynamicM[idx] <= 0 ? 0 : (dynamicDamage + coreDamage) / (coreM[idx] + dynamicM[idx]) * tKe[idx];
            psionLevel[idx] -= deltaPsion;
            tKe[idx] += -deltaPhonon;
            if (!chunk)
            {
                stomachM += dynamicDamage + coreDamage;//add organ bruising cascade
                psionLevel[idx] += deltaPsion;
                tKe[sI] += deltaPhonon;
            }
            dynamicM[idx] -= dynamicDamage;
            coreM[idx] -= coreDamage;
            healthiness[idx] = (coreM[idx] + dynamicM[idx]) / startHealth[idx];
            if (leftOverCore > 0)
            {
                if (idx == sI)
                {
                    Console.WriteLine("Total loss of functional organ");
                }
                else
                    bruise(0, leftOverCore, sI, chunk);
            }
            if (coreM[idx] + dynamicM[idx] == 0)
            {
                // Debug.LogError("ORGAN DEATH");
            }
        }

        float contaminationRate
        {
            get
            {
                return (float)Math.Exp(-16 / beta) / 4;
            }
        }
        internal float getTemperature(int idx)
        {
            if (idx == sI)
                return tKe[idx] / (coreM[idx] + dynamicM[idx] + stomachM + fatM);
            else
                return tKe[idx] / (coreM[idx] + dynamicM[idx]);
        }

        float bps
        {
            get
            {

                float realBps = (float)Math.Round(baseBps * getTemperature(sI) / EnergyManager.roomTemp * currentPower[sI] * healthiness[sI], 3);
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
                        if (tKe[sI] == 0)
                            Console.WriteLine("Death by Hypothermia");
                        else if (currentPower[sI] == 0)
                            Console.WriteLine("Death by Energy Deficiency");
                        else
                            Console.WriteLine("Death by Cardiac Damage");
                        return 1;
                    }
                    else
                    {
                        return realBps;
                    }
                }

            }
        }

        internal IEnumerable<float> Discrete()
        {

            //is s.startSimulation
            //Pipe Demands
            totalDemandP = maxM.Sum();
            sDP = maxM[sI] / totalDemandP;
            cDP = maxM[cI] / totalDemandP;
            bDP = maxM[bI] / totalDemandP;
            pDP = maxM[pI] / totalDemandP;
            mDP = maxM[mI] / totalDemandP;
            vDP = maxM[vI] / totalDemandP;
            wDP = maxM[wI] / totalDemandP;

            //Power demands
            totalDemandE = pC.Sum();
            sDE = pC[sI] / totalDemandE;
            pDE = pC[pI] / totalDemandE;
            mDE = pC[mI] / totalDemandE;
            vDE = pC[vI] / totalDemandE;
            wDE = pC[wI] / totalDemandE;
            yield return 0;

            while (true)
            {
                float time = 0;
                curBps = bps;

                //this is s.flowIn()
                toProcessMTP[sI] = inMTP[sI];
                toProcessMTP[vI] = inMTP[vI];
                toProcessMTP[bI] = inMTP[bI];
                toProcessMTP[cI] = inMTP[cI];
                toProcessMTP[wI] = inMTP[wI];
                toProcessMTP[mI] = inMTP[mI];
                toProcessMTP[pI] = inMTP[pI];
              
                outMTP[sI] = inMTP[sI] = Vector3.Zero;
                outMTP[vI] = inMTP[vI] = Vector3.Zero;
                outMTP[bI] = inMTP[bI] = Vector3.Zero;
                outMTP[cI] = inMTP[cI] = Vector3.Zero;
                outMTP[wI] = inMTP[wI] = Vector3.Zero;
                outMTP[mI] = inMTP[mI] = Vector3.Zero;
                outMTP[pI] = inMTP[pI] = Vector3.Zero;

                if (time >= 1 / curBps)
                {
                    yield return 0;
                }
                else
                {
                    while (time < 1 / curBps)
                    {

                        //this is s.continuousIn
                        usedQ[sI] = Math.Min(pC[sI] / healthiness[sI], inQ[sI]);//get max of inQ and power necessary to meet powerconsumption demand after heating
                        tKe[sI] += (usedQ[sI] - (usedQ[sI] * healthiness[sI]));//ohmic heating (using power because the overall result would be described as the sum, or in calculus the integral.
                        outQ[sI] = inQ[sI] - usedQ[sI];//amount of energy left over
                        currentPower[sI] = (usedQ[sI] * healthiness[sI]) / pC[sI];
                        inQ[sI] = 0;


                        //this is v.continuousIn
                        usedQ[vI] = Math.Min(pC[vI] / healthiness[vI], inQ[vI]);//get max of inQ and power necessary to meet powerconsumption demand after heating
                        tKe[vI] += (usedQ[vI] - (usedQ[vI] * healthiness[vI]));//ohmic heating (using power because the overall result would be described as the sum, or in calculus the integral.
                        outQ[vI] = inQ[vI] - usedQ[vI];//amount of energy left over
                        currentPower[vI] = (usedQ[vI] * healthiness[vI]) / pC[vI];
                        inQ[vI] = 0;

                        //b.continuousIn() does nothing

                        //this is c.continuousIn
                        charge[cI] += inQ[cI];
                        charge[cI] = Math.Min(maxCharge[cI], charge[cI]);
                        inQ[cI] = 0;

                        //this is w.continuousIn()
                        outQ[wI] = inQ[wI] - usedQ[wI];
                        currentPower[wI] = usedQ[wI] / pC[wI];
                        inQ[wI] = 0;


                        //this is m.continuousIn()
                        outQ[mI] = inQ[mI] - usedQ[mI];
                        if (outQ[mI] > 0)
                        {
                            if (charge[mI] < maxCharge[mI])
                            {
                                float deltaQ = Math.Min(maxCharge[mI] - charge[mI], outQ[mI]);
                                outQ[mI] -= deltaQ;
                                charge[mI] += deltaQ;
                                usedQ[mI] += deltaQ;
                            }
                        }
                        currentPower[mI] = usedQ[mI] / pC[mI];
                        inQ[mI] = 0;


                        //this is p.continuousIn
                        usedQ[pI] = Math.Min(pC[pI] / healthiness[pI], inQ[pI]);//get max of inQ and power necessary to meet powerconsumption demand after heating
                        tKe[pI] += (usedQ[pI] - (usedQ[pI] * healthiness[pI]));//ohmic heating (using power because the operall result would be described as the sum, or in calculus the integral.
                        outQ[pI] = inQ[pI] - usedQ[pI];//amount of energy left oper
                        currentPower[pI] = (usedQ[pI] * healthiness[pI]) / pC[pI];
                        inQ[pI] = 0;



                        //this is v.absorb()
                        float minMv = Math.Min(metabolism[vI], toProcessMTP[vI].X);
                        Vector3 deltaMTPv = Vector3.Zero;
                        if (minMv > 0)
                        {
                            deltaMTPv = minMv / toProcessMTP[vI].X * toProcessMTP[vI];
                            psionLevel[vI] += deltaMTPv.Z;
                            float finalTemp = (tKe[vI] + deltaMTPv.Y) / (coreM[vI] + dynamicM[vI] + deltaMTPv.X);
                            toProcessMTP[vI] -= deltaMTPv;
                            deltaMTPv = new Vector3(deltaMTPv.X, 0, 0);
                            float usedM = Math.Min(deltaMTPv.X, startHealth[vI] - (coreM[vI] + dynamicM[vI]));//if health below normal, get minimum of health needed, available matter, and regeneration
                            dynamicM[vI] += usedM;
                            tKe[vI] = finalTemp * (coreM[vI] + dynamicM[vI]);
                            deltaMTPv -= deltaMTPv * usedM / deltaMTPv.X;
                            deltaMTPv.Y = deltaMTPv.X * finalTemp;
                        }
                        healthiness[vI] = (dynamicM[vI] + coreM[vI]) / startHealth[vI];
                        outMTP[vI] += deltaMTPv;


                        //this is b.absorb()
                        float finalTempB = getTemperature(bI);
                        float minMb = Math.Min(metabolism[bI], toProcessMTP[bI].X);
                        Vector3 deltaMTPb = Vector3.Zero;
                        if (minMb > 0)
                        {
                            deltaMTPb = minMb / toProcessMTP[bI].X * toProcessMTP[bI];
                            psionLevel[bI] += deltaMTPb.Z;
                            finalTempB = (tKe[bI] + deltaMTPb.Y) / (coreM[bI] + dynamicM[bI] + deltaMTPb.X);
                            toProcessMTP[bI] -= deltaMTPb;
                            deltaMTPb = new Vector3(deltaMTPb.X, 0, 0);
                        }
                        bruise(0, startHealth[bI] * contaminationRate, bI);//damaged with contamination (at end because this is cobered up by inM in flowing
                        if (minMb > 0)
                        {
                            float usedM = Math.Min(deltaMTPb.X, startHealth[bI] - (coreM[bI] + dynamicM[bI]));//if health below normal, get minimum of health needed, abailable matter, and regeneration
                            dynamicM[bI] += usedM;
                            tKe[bI] = finalTempB * (coreM[bI] + dynamicM[bI]);
                            deltaMTPb -= deltaMTPb * usedM / deltaMTPb.X;
                            deltaMTPb.Y = deltaMTPb.X * finalTempB;
                        }
                        healthiness[bI] = (dynamicM[bI] + coreM[bI]) / startHealth[bI];
                        outMTP[bI] += deltaMTPb;


                        //this is c.absorb()
                        float minMc = Math.Min(metabolism[cI], toProcessMTP[cI].X);
                        Vector3 deltaMTPc = Vector3.Zero;
                        if (minMc > 0)
                        {
                            deltaMTPc = minMc / toProcessMTP[cI].X * toProcessMTP[cI];
                            psionLevel[cI] += deltaMTPc.Z;
                            float finalTemp = (tKe[cI] + deltaMTPc.Y) / (coreM[cI] + dynamicM[cI] + deltaMTPc.X);
                            toProcessMTP[cI] -= deltaMTPc;
                            deltaMTPc = new Vector3(deltaMTPc.X, 0, 0);
                            float usedM = Math.Min(deltaMTPc.X, startHealth[cI] - (coreM[cI] + dynamicM[cI]));//if health below normal, get minimum of health needed, available matter, and regeneration
                            dynamicM[cI] += usedM;
                            tKe[cI] = finalTemp * (coreM[cI] + dynamicM[cI]);
                            deltaMTPc -= deltaMTPc * usedM / deltaMTPc.X;
                            deltaMTPc.Y = deltaMTPc.X * finalTemp;
                        }
                        healthiness[cI] = (dynamicM[cI] + coreM[cI]) / startHealth[cI];
                        outMTP[cI] += deltaMTPc;


                        //this is w.absorb()
                        float rawPsions = 0;
                        float minMw = Math.Min(metabolism[wI], toProcessMTP[wI].X);
                        Vector3 deltaMTPw = Vector3.Zero;
                        if (minMw > 0)
                        {
                            deltaMTPw = minMw / toProcessMTP[wI].X * toProcessMTP[wI];
                            rawPsions = deltaMTPw.Z;
                            float finalTemp = (tKe[wI] + deltaMTPw.Y) / (coreM[wI] + dynamicM[wI] + deltaMTPw.X);
                            toProcessMTP[wI] -= deltaMTPw;
                            deltaMTPw = new Vector3(deltaMTPw.X, 0, 0);
                            float usedM = Math.Min(deltaMTPw.X, startHealth[wI] - (coreM[wI] + dynamicM[wI]));//if health below normal, get minimum of health needed, awailable matter, and regeneration
                            dynamicM[wI] += usedM;
                            tKe[wI] = finalTemp * (coreM[wI] + dynamicM[wI]);
                            deltaMTPw -= deltaMTPw * usedM / deltaMTPw.X;
                            deltaMTPw.Y = deltaMTPw.X * finalTemp;
                        }
                        if (usePsions)
                        {
                            crystalPsionw += rawPsions;
                            rawPsions = 0;
                            usePsions = false;
                        }
                        healthiness[wI] = (dynamicM[wI] + coreM[wI]) / startHealth[wI];
                        outMTP[wI] += deltaMTPw;
                        psionLevel[vI] += rawPsions;

                        //this is m.absorb()
                        float minMm = Math.Min(metabolism[mI], toProcessMTP[mI].X);
                        Vector3 deltaMTPm = Vector3.Zero;
                        if (minMm > 0)
                        {
                            deltaMTPm = minMm / toProcessMTP[mI].X * toProcessMTP[mI];
                            psionLevel[mI] += deltaMTPm.Z;
                            float finalTemp = (tKe[mI] + deltaMTPm.Y) / (coreM[mI] + dynamicM[mI] + deltaMTPm.X);
                            toProcessMTP[mI] -= deltaMTPm;
                            deltaMTPm = new Vector3(deltaMTPm.X, 0, 0);
                            float usedM = Math.Min(deltaMTPm.X, startHealth[mI] - (coreM[mI] + dynamicM[mI]));//if health below normal, get minimum of health needed, available matter, and regeneration
                            dynamicM[mI] += usedM;
                            tKe[mI] = finalTemp * (coreM[mI] + dynamicM[mI]);
                            deltaMTPm -= deltaMTPm * usedM / deltaMTPm.X;
                            deltaMTPm.Y = deltaMTPm.X * finalTemp;
                        }
                        healthiness[mI] = (dynamicM[mI] + coreM[mI]) / startHealth[mI];
                        outMTP[mI] += deltaMTPm;


                        float minMp = Math.Min(metabolism[pI], toProcessMTP[pI].X);
                        Vector3 deltaMTPp = Vector3.Zero;
                        if (minMp > 0)
                        {
                            deltaMTPp = minMp / toProcessMTP[pI].X * toProcessMTP[pI];
                            psionLevel[pI] += deltaMTPp.Z;
                            float finalTemp = (tKe[pI] + deltaMTPp.Y) / (coreM[pI] + dynamicM[pI] + deltaMTPp.X);
                            toProcessMTP[pI] -= deltaMTPp;
                            deltaMTPp = new Vector3(deltaMTPp.X, 0, 0);
                            float usedM = Math.Min(deltaMTPp.X, startHealth[pI] - (coreM[pI] + dynamicM[pI]));//if health below normal, get minimum of health needed, apailable matter, and regeneration
                            dynamicM[pI] += usedM;
                            tKe[pI] = finalTemp * (coreM[pI] + dynamicM[pI]);
                            deltaMTPp -= deltaMTPp * usedM / deltaMTPp.X;
                            deltaMTPp.Y = deltaMTPp.X * finalTemp;
                        }
                        healthiness[pI] = (dynamicM[pI] + coreM[pI]) / startHealth[pI];
                        float toCpt = Math.Min(stomachM, drain) * currentPower[pI];
                        stomachM -= toCpt;
                        dynamicM[sI] += toCpt;
                        float realP = toCpt * EnergyManager.psionPerKg;
                        outMTP[pI].Z += realP * healthiness[pI];
                        psionLevel[pI] += realP * (1 - healthiness[pI]);
                        outMTP[pI] += deltaMTPp;

                        absorb();




                        //this is v.continuousOut
                        inQ[cI] += outQ[vI];
                        usedQ[vI] = 0;


                        //this is b.continuousOut()
                        float realFlow = beta;
                        bodyCharge += -realFlow * (1 - healthiness[bI]);//leak charge into body
                        inQ[cI] += realFlow * healthiness[bI];

                        //this is c.continuousOut
                        float rawQ = Math.Min(totalDemandE / healthiness[cI], charge[cI]);//Take as much eneryg as needed (taking into account heating)
                        charge[cI] -= rawQ;
                        float netQ = rawQ * healthiness[cI];
                        outQ[cI] = netQ;
                        if (coreM[cI] + dynamicM[cI] > 0)
                            tKe[cI] += rawQ - netQ;//ohmic heating (using power because the overall result would be described as the sum, or in calculus the integral.
                        inQ[wI] = netQ * wDE;
                        inQ[mI] = netQ * mDE;
                        inQ[vI] = netQ * vDE;
                        inQ[pI] = netQ * pDE;
                        inQ[sI] = netQ * sDE;


                        //this is w.continuousOut
                        inQ[cI] += outQ[wI];
                        usedQ[wI] = 0;


                        //this is m.continuousOut
                        inQ[cI] += outQ[mI];
                        usedQ[mI] = 0;

                        //this is p.continuousOut
                        inQ[cI] += outQ[pI];
                        usedQ[pI] = 0;

                        //this is s.continuousOut
                        inQ[cI] += outQ[sI];
                        usedQ[sI] = 0;


                        time += 0.05f;

                        yield return .05f;
                    }


                    float psionsReleasedv = 0;
                    float psionsReleasedb = 0;
                    float psionsReleasedc = 0;
                    float psionsReleasedw = 0;
                    float psionsReleasedm = 0;
                    float psionsReleasedp = 0;
                    float psionsReleaseds = psionLevel[sI] / coreM[sI] * outMTP[sI].X;

                    if (coreM[vI] > 0)
                    {
                        psionsReleasedv = psionLevel[vI] / coreM[vI] * outMTP[vI].X * healthiness[vI];
                    }
                    if (coreM[bI] > 0)
                    {
                        psionsReleasedb = psionLevel[bI] / coreM[bI] * outMTP[bI].X * healthiness[bI];
                    }
                    if (coreM[cI] > 0)
                    {
                        psionsReleasedc = psionLevel[cI] / coreM[cI] * outMTP[cI].X * healthiness[cI];
                    }
                    if (coreM[wI] > 0)
                    {
                        psionsReleasedw = psionLevel[wI] / coreM[wI] * outMTP[wI].X * healthiness[wI];
                    }
                    if (coreM[pI] > 0)
                    {
                        psionsReleasedp = psionLevel[pI] / coreM[pI] * outMTP[pI].X * healthiness[pI];
                    }
                    if (coreM[mI] > 0)
                    {
                        psionsReleasedm = psionLevel[mI] / coreM[mI] * outMTP[mI].X * healthiness[mI];
                    }
                    psionLevel[vI] -= psionsReleasedv;
                    psionLevel[bI] -= psionsReleasedb;
                    psionLevel[cI] -= psionsReleasedc;
                    psionLevel[wI] -= psionsReleasedw;
                    psionLevel[mI] -= psionsReleasedm;
                    psionLevel[pI] -= psionsReleasedp;
                    psionLevel[sI] -= psionsReleaseds;

                    outMTP[vI] += new Vector3(0, 0, psionsReleasedv);
                    outMTP[bI] += new Vector3(0, 0, psionsReleasedb);
                    outMTP[cI] += new Vector3(0, 0, psionsReleasedc);
                    outMTP[wI] += new Vector3(0, 0, psionsReleasedw);
                    outMTP[mI] += new Vector3(0, 0, psionsReleasedm);
                    outMTP[pI] += new Vector3(0, 0, psionsReleasedp);
                    outMTP[sI] += new Vector3(0, 0, psionsReleaseds) + toProcessMTP[sI];

                    inMTP[sI] += (outMTP[vI] + toProcessMTP[vI]) * healthiness[vI];
                    inMTP[sI] += (outMTP[bI] + toProcessMTP[bI]) * healthiness[bI];
                    inMTP[sI] += (outMTP[cI] + toProcessMTP[cI]) * healthiness[cI];
                    inMTP[sI] += (outMTP[wI] + toProcessMTP[wI]) * healthiness[wI];
                    inMTP[sI] += (outMTP[mI] + toProcessMTP[mI]) * healthiness[mI];
                    inMTP[sI] += (outMTP[pI] + toProcessMTP[pI]) * healthiness[pI];

                    stomachM += (outMTP[vI].X + toProcessMTP[vI].X) * (1 - healthiness[vI]);
                    stomachM += (outMTP[bI].X + toProcessMTP[bI].X) * (1 - healthiness[bI]);
                    stomachM += (outMTP[cI].X + toProcessMTP[cI].X) * (1 - healthiness[cI]);
                    stomachM += (outMTP[wI].X + toProcessMTP[wI].X) * (1 - healthiness[wI]);
                    stomachM += (outMTP[mI].X + toProcessMTP[mI].X) * (1 - healthiness[mI]);
                    stomachM += (outMTP[pI].X + toProcessMTP[pI].X) * (1 - healthiness[pI]);

                    psionLevel[sI] += (outMTP[vI].Z + toProcessMTP[vI].Z) * (1 - healthiness[vI]);
                    psionLevel[sI] += (outMTP[bI].Z + toProcessMTP[bI].Z) * (1 - healthiness[bI]);
                    psionLevel[sI] += (outMTP[cI].Z + toProcessMTP[cI].Z) * (1 - healthiness[cI]);
                    psionLevel[sI] += (outMTP[wI].Z + toProcessMTP[wI].Z) * (1 - healthiness[wI]);
                    psionLevel[sI] += (outMTP[mI].Z + toProcessMTP[mI].Z) * (1 - healthiness[mI]);
                    psionLevel[sI] += (outMTP[pI].Z + toProcessMTP[pI].Z) * (1 - healthiness[pI]);

                    tKe[sI] += (outMTP[vI].Y + toProcessMTP[vI].Y) * (1 - healthiness[vI]);
                    tKe[sI] += (outMTP[bI].Y + toProcessMTP[bI].Y) * (1 - healthiness[bI]);
                    tKe[sI] += (outMTP[cI].Y + toProcessMTP[cI].Y) * (1 - healthiness[cI]);
                    tKe[sI] += (outMTP[wI].Y + toProcessMTP[wI].Y) * (1 - healthiness[wI]);
                    tKe[sI] += (outMTP[mI].Y + toProcessMTP[mI].Y) * (1 - healthiness[mI]);
                    tKe[sI] += (outMTP[pI].Y + toProcessMTP[pI].Y) * (1 - healthiness[pI]);

                    //this is s.flowOut()
                    inMTP[cI] = outMTP[sI] * cDP;
                    inMTP[wI] = outMTP[sI] * wDP;
                    inMTP[mI] = outMTP[sI] * mDP;
                    inMTP[vI] = outMTP[sI] * vDP;
                    inMTP[pI] = outMTP[sI] * pDP;
                    inMTP[bI] = outMTP[sI] * bDP;
                    inMTP[sI] += outMTP[sI] * sDP;

                    //Console.WriteLine(inMTP[sI] + " " + inMTP[bI] + " " + inMTP[pI]);

                }
                yield return .01f;
            }
        }

    }




}
