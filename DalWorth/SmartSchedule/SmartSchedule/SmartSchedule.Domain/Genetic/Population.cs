using System;
using System.Collections.Generic;
using System.Text;

namespace SmartSchedule.Domain.Genetic
{
    public class Population
    {
        public delegate void InitialChromosomeGeneratedHandler(int totalChromosomesGenerated);
        public event InitialChromosomeGeneratedHandler InitialChromosomeGenerated;
        
        private readonly int m_populationSize;
        private readonly int m_selectionSize;
        private readonly int m_childrenPerGeneration;
        private readonly int m_mutationsPerGeneration;
        private readonly int m_mutationStrangthPercentage;

        private readonly BookingEngine m_bookingEngine;
        private readonly List<Chromosome> m_chromosomes;

        #region BestChromosome

        private Chromosome m_bestChromosome;
        public Chromosome BestChromosome
        {
            get { return m_bestChromosome; }
        }

        #endregion

        #region Constructor

        public Population(BookingEngine bookingEngine, int populationSize, int selectionSize, 
            int childrenPerGeneration, int mutationsPerGeneration, int mutationStrangthPercentage)
        {
            m_bookingEngine = bookingEngine;

            m_populationSize = populationSize;
            m_selectionSize = selectionSize;
            m_childrenPerGeneration = childrenPerGeneration;
            m_mutationsPerGeneration = mutationsPerGeneration;
            m_mutationStrangthPercentage = mutationStrangthPercentage;
            m_chromosomes = new List<Chromosome>();
            InitDistributionGoal();
        }

        #endregion

        #region FillRandom

        public void FillRandom()
        {
            for (int i = 0; i < m_populationSize; i++)
            {
                Chromosome chromosome = new Chromosome(m_bookingEngine);
                chromosome.Fill(true);
                m_chromosomes.Add(chromosome);

                if (InitialChromosomeGenerated != null)
                    InitialChromosomeGenerated(m_chromosomes.Count);
            }

            OrderChromosomesByFitnessValue();            
        }

        #endregion

        #region Selection

        private List<Chromosome> Selection()
        {
            double minFitnessValue = m_chromosomes[0].FitnessValue.TotalValue;
            double maxFitnessValue = m_chromosomes[m_chromosomes.Count - 1].FitnessValue.TotalValue;
            double difference = maxFitnessValue - minFitnessValue;

            double multipliersSum = 0;
            foreach (Chromosome chromosome in m_chromosomes)
            {
                multipliersSum += (maxFitnessValue - chromosome.FitnessValue.TotalValue)/difference;
            }

            double basePercentage = 10000/multipliersSum;

            Dictionary<Chromosome, int> probabilityMap = new Dictionary<Chromosome, int>();
            foreach (Chromosome chromosome in m_chromosomes)
            {
                probabilityMap.Add(chromosome,
                    (int)(basePercentage * ((maxFitnessValue - chromosome.FitnessValue.TotalValue) / difference)));
            }


            RandomEx random = new RandomEx();

            int nonZeroProbabilityChromosomesCount = 0;
            foreach (int probability in probabilityMap.Values)
            {
                if (probability != 0)
                    nonZeroProbabilityChromosomesCount++;
            }

            List<Chromosome> result = new List<Chromosome>();
            while (result.Count < Math.Min(m_selectionSize, nonZeroProbabilityChromosomesCount))
            {
                Chromosome chromosome = random.Next(probabilityMap);

                if (!result.Contains(chromosome))
                    result.Add(chromosome);
            }

            return result;
        }

        #endregion

        #region Crossover

        private Chromosome Crossover(Chromosome parent1, Chromosome parent2)
        {
            Chromosome result = (Chromosome) parent1.Clone();
            Technician technicianToSwap = null;

            ListEx<Technician> technicians = new ListEx<Technician>(m_bookingEngine.GetTechnicians(DateTime.Now));
            technicians.Shuffle();

            foreach (Technician technician in technicians)
            {
                if (parent2.CalculateFitnessValue(technician) < parent1.CalculateFitnessValue(technician))
                {
                    technicianToSwap = technician;
                    break;
                }
            }

            if (technicianToSwap == null)
                return null;

            List<Visit> visitsExistInParent1Only = new List<Visit>();
            List<Visit> visitsExistInParent2Only = new List<Visit>();

            foreach (Visit visit in result.GetVisits(technicianToSwap))
            {
                bool isExistInParent2 = false;

                foreach (Visit visitInner in parent2.GetVisits(technicianToSwap))
                {
                    if (visit.ID == visitInner.ID)
                    {
                        isExistInParent2 = true;
                        break;
                    }
                }

                if (!isExistInParent2)
                    visitsExistInParent1Only.Add(visit);
            }

            foreach (Visit visit in parent2.GetVisits(technicianToSwap))
            {
                bool isExistInParent1 = false;

                foreach (Visit visitInner in parent1.GetVisits(technicianToSwap))
                {
                    if (visit.ID == visitInner.ID)
                    {
                        isExistInParent1 = true;
                        break;
                    }
                }

                if (!isExistInParent1)
                    visitsExistInParent2Only.Add(visit);
            }

            if (visitsExistInParent2Only.Count > 0)
                result.RemoveVisitsById(visitsExistInParent2Only);

            result.GetVisits(technicianToSwap).Clear();
            foreach (Visit visit in parent2.GetVisits(technicianToSwap))
            {
                result.InsertVisitById(visit, technicianToSwap);
            }

            if (visitsExistInParent1Only.Count > 0
                && !result.InsertVisitsMinizingCost(visitsExistInParent1Only, technicianToSwap))
            {
                return null;
            }

            return result;
        }

        #endregion

        #region OrderChromosomesByFitnessValue

        private void OrderChromosomesByFitnessValue()
        {
            m_chromosomes.Sort(delegate(Chromosome x, Chromosome y)
                               {
                                   return x.FitnessValue.TotalValue.CompareTo(y.FitnessValue.TotalValue);
                               });
        }

        #endregion

        #region NextGeneration

        public void NextGeneration()
        {
            List<Chromosome> selectedChromosomes = Selection();

            Dictionary<Chromosome, List<Chromosome>> possibleParents 
                = new Dictionary<Chromosome, List<Chromosome>>();

            foreach (Chromosome chromosome in selectedChromosomes)
            {
                List<Chromosome> secondParents = new List<Chromosome>(selectedChromosomes);
                secondParents.Remove(chromosome);
                possibleParents.Add(chromosome, secondParents);
            }

            Random random = new Random();

            List<Chromosome> children = new List<Chromosome>();
            List<Chromosome> parent1Candidates = new List<Chromosome>(selectedChromosomes);
            while (children.Count < m_childrenPerGeneration)
            {
                if (parent1Candidates.Count == 0)
                    break;

                Chromosome parent1 = parent1Candidates[random.Next(0, parent1Candidates.Count)];
                List<Chromosome> parent2Candidates = possibleParents[parent1];
                if (parent2Candidates.Count == 0)
                {
                    parent1Candidates.Remove(parent1);
                    continue;
                }
                    
                Chromosome parent2 = parent2Candidates[random.Next(0, parent2Candidates.Count)];
                parent2Candidates.Remove(parent2);

                Chromosome child;

                if (parent1.FitnessValue.TotalValue < parent2.FitnessValue.TotalValue)
                    child = Crossover(parent1, parent2);
                else
                    child = Crossover(parent2, parent1);

                if (child != null)
                    children.Add(child);
            }

            m_chromosomes.AddRange(children);
            OrderChromosomesByFitnessValue();

            if (m_bestChromosome == null
                || m_bestChromosome.FitnessValue.TotalValue > m_chromosomes[0].FitnessValue.TotalValue)
            {
                m_bestChromosome = (Chromosome) m_chromosomes[0].Clone();
            }

            m_chromosomes.RemoveRange(m_populationSize, m_chromosomes.Count - m_populationSize);
            Mutate();
            OrderChromosomesByFitnessValue();
        }

        #endregion

        #region Mutate

        private void Mutate()
        {            
            Random random = new Random();

            List<Chromosome> chromosomesToMutate = new List<Chromosome>();

            while (chromosomesToMutate.Count < m_mutationsPerGeneration)
            {
                Chromosome candidate = m_chromosomes[random.Next(0, m_chromosomes.Count)];

                if (!chromosomesToMutate.Contains(candidate))
                    chromosomesToMutate.Add(candidate);
            }

            foreach (Chromosome chromosome in chromosomesToMutate)
            {
                chromosome.Mutate(m_mutationStrangthPercentage);
            }
        }

        #endregion

        #region InitDistributionGoal

        private void InitDistributionGoal()
        {
            decimal visitsTotalCost = decimal.Zero;
            foreach (Visit visit in m_bookingEngine.Visits)
                visitsTotalCost += visit.Cost;

            List<Technician> technicians = new List<Technician>(m_bookingEngine.GetTechnicians(DateTime.Now.Date));

            while (true)
            {
                decimal techniciansTotalHourlyRate = decimal.Zero;
                foreach (Technician technician in technicians)
                {
                    technician.DistributionCostTarget = decimal.Zero;
                    techniciansTotalHourlyRate += technician.HourlyRate;
                }

                double averageHours = (double)(visitsTotalCost / techniciansTotalHourlyRate);
                List<Technician> smallCapacityTechnicians = new List<Technician>();
                foreach (Technician technician in technicians)
                {
                    if (technician.GetWorkDayDurationHours() < averageHours)
                    {
                        technician.DistributionCostTarget
                            = (decimal)technician.GetWorkDayDurationHours() * technician.HourlyRate;
                        smallCapacityTechnicians.Add(technician);
                        visitsTotalCost -= technician.DistributionCostTarget;
                    }
                }

                foreach (Technician technician in smallCapacityTechnicians)
                    technicians.Remove(technician);

                if (smallCapacityTechnicians.Count == 0)
                {
                    foreach (Technician technician in technicians)
                        technician.DistributionCostTarget = (decimal)averageHours * technician.HourlyRate;
                    break;
                }
            }            
        }

        #endregion

    }
}
