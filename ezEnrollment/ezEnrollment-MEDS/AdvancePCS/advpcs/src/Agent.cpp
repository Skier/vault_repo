/*
 *  $RCSfile: Agent.cpp,v $
 *
 *  $Revision: 1.2 $
 *
 *  last change: $Date: 2003/09/03 15:19:46 $
 */

#include "wx/wxprec.h"

#ifdef __BORLANDC__
    #pragma hdrstop
#endif

#ifndef WX_PRECOMP
#include <wx/wx.h>
#endif

/* -------------------------- header place ---------------------------------- */
#include <atf/Cfg.h>
#include <advpcs/Agent.h>
#include <advpcs/Resources.h>
/* -------------------------- implementation place -------------------------- */
Agent::Agent(ICfg& cfg, ILogger& logger) 
    : m_logger(logger), m_cfg(cfg), m_enabled(false) 
{
    m_enabled = cfg.GetParamAsBool(ADVPCS_AGENT_ENABLE_CFG, false);
};
