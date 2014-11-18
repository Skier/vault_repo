using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using SmartSchedule.Domain;
using SmartSchedule.Domain.Genetic;
using SmartSchedule.Win32.MainForm;
using SmartSchedule.Windows;

namespace SmartSchedule.Win32.Genetic
{
    public class GeneticModel : IModel
    {
        #region BookingEngine

        private BookingEngine m_bookingEngine;
        public BookingEngine BookingEngine
        {
            get { return m_bookingEngine; }
            set { m_bookingEngine = value; }
        }

        #endregion

        #region CurrentSolution

        private Chromosome m_currentSolution;
        public Chromosome CurrentSolution
        {
            get { return m_currentSolution; }
            set { m_currentSolution = value; }
        }

        #endregion

        #region Init

        public void Init()
        {
        }

        #endregion

        #region GetInitialFitnessValue

        public Fitness GetInitialFitnessValue()
        {
            Chromosome chromosome = new Chromosome(m_bookingEngine);
            chromosome.Fill(false);
            return chromosome.FitnessValue;
        }

        #endregion

    }
}
