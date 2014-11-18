using System;
using System.Collections.Generic;
using System.Text;

namespace dalworth.domain
{
    public enum RugShape
    {
        Rectangle,
        Round
    }
    
    public class Rug
    {
        private const decimal PRICE_PER_SQ_FOOT_CLEAN = 3;
        private const decimal PRICE_PER_SQ_FOOT_PROTECTOR = 1.15M;
        private const decimal PRICE_PER_SQ_FOOT_PADDING = 2;
        private const decimal PRICE_PER_SQ_FOOT_MOTH_REPEL = 0.35M;
        private const decimal PRICE_RAP = 8;
        
        private const decimal TAX_PERCENT = 0.05M;
        
        private RugShape m_shape;
        private decimal m_width;
        private decimal m_height;
        private decimal m_diameter;

        private bool m_isCleanApplied;
        private bool m_isProtectorApplied;
        private bool m_isPaddingApplied;
        private bool m_isMothRepelApplied;
        private bool m_isRapApplied;

        private decimal m_otherCost;

        public Rug(RugShape shape, decimal width, decimal height, decimal diameter, bool isCleanApplied, bool isProtectorApplied, bool isPaddingApplied, bool isMothRepelApplied, bool isRapApplied, decimal otherPrice)
        {
            m_shape = shape;
            m_width = width;
            m_height = height;
            m_diameter = diameter;
            m_isCleanApplied = isCleanApplied;
            m_isProtectorApplied = isProtectorApplied;
            m_isPaddingApplied = isPaddingApplied;
            m_isMothRepelApplied = isMothRepelApplied;
            m_isRapApplied = isRapApplied;
            m_otherCost = otherPrice;
            
            RecalculateFootage();
        }

        public Rug(Rug rug)
        {
            m_shape = rug.Shape;
            m_width = rug.Width;
            m_height = rug.Height;
            m_diameter = rug.Diameter;
            m_isCleanApplied = rug.IsCleanApplied;
            m_isProtectorApplied = rug.IsProtectorApplied;
            m_isPaddingApplied = rug.IsPaddingApplied;
            m_isMothRepelApplied = rug.IsMothRepelApplied;
            m_isRapApplied = rug.IsRapApplied;
            m_otherCost = rug.OtherCost;

            RecalculateFootage();            
        }

        decimal m_currentSquareFootage;

        public RugShape Shape
        {
            get { return m_shape; }
            set 
            { 
                m_shape = value; 
                RecalculateFootage();
            }
        }

        public decimal Width
        {
            get { return m_width; }
            set
            {
                m_width = value;
                RecalculateFootage();
            }
        }

        public decimal Height
        {
            get { return m_height; }
            set
            {
                m_height = value;
                RecalculateFootage();
            }
        }

        public decimal Diameter
        {
            get { return m_diameter; }
            set
            {
                m_diameter = value;
                RecalculateFootage();
            }
        }

        public bool IsCleanApplied
        {
            get { return m_isCleanApplied; }
            set { m_isCleanApplied = value; }
        }

        public bool IsProtectorApplied
        {
            get { return m_isProtectorApplied; }
            set { m_isProtectorApplied = value; }
        }

        public bool IsPaddingApplied
        {
            get { return m_isPaddingApplied; }
            set { m_isPaddingApplied = value; }
        }

        public bool IsMothRepelApplied
        {
            get { return m_isMothRepelApplied; }
            set { m_isMothRepelApplied = value; }
        }

        public bool IsRapApplied
        {
            get { return m_isRapApplied; }
            set { m_isRapApplied = value; }
        }

        public decimal OtherCost
        {
            get { return m_otherCost; }
            set { m_otherCost = value; }
        }
        
        private void RecalculateFootage()
        {
            if (m_shape == RugShape.Rectangle)
            {
                m_currentSquareFootage = m_height * m_width;
            }
            else if (m_shape == RugShape.Round)
            {
                m_currentSquareFootage = (decimal)Math.PI * (m_diameter * m_diameter / 4);
            } else
            {
                m_currentSquareFootage = decimal.Zero;            
            }                        
        }
        
        public decimal SquareFootage
        {
            get
            {
                return m_currentSquareFootage;
            }
        }
        
        public decimal CleanCost
        {
            get
            {
                if (m_isCleanApplied)
                    return SquareFootage*PRICE_PER_SQ_FOOT_CLEAN;
                return decimal.Zero;
            }
        }
        
        public decimal ProtectorCost
        {
            get
            {
                if (m_isProtectorApplied)
                    return SquareFootage*PRICE_PER_SQ_FOOT_PROTECTOR;
                return decimal.Zero;
            }
        }
         
        public decimal PaddingCost
        {
            get
            {
                if (m_isPaddingApplied)
                    return SquareFootage*PRICE_PER_SQ_FOOT_PADDING;
                return decimal.Zero;
            }
        }
        
        public decimal MothRepelCost
        {
            get
            {
                if (m_isMothRepelApplied)
                    return SquareFootage*PRICE_PER_SQ_FOOT_MOTH_REPEL;
                return decimal.Zero;
            }
        }
        
        public decimal RapCost
        {
            get
            {
                if (m_isRapApplied)
                    return PRICE_RAP;
                return decimal.Zero;
            }
        }
        
        public decimal TotalCost
        {
            get
            {
                return CleanCost
                       + ProtectorCost
                       + PaddingCost
                       + MothRepelCost
                       + RapCost
                       + m_otherCost;
            }
        }
        
        public decimal TaxAmount
        {
            get
            {
                return TotalCost*TAX_PERCENT;
            }
        }
        
        public decimal TotalWithTaxCost
        {
            get
            {
                return TotalCost + TaxAmount;
            }
        }
    }
}
