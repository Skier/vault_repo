package com.llsvc.server.framework;

import java.io.IOException;
import javax.servlet.Filter;
import javax.servlet.FilterChain;
import javax.servlet.FilterConfig;
import javax.servlet.ServletException;
import javax.servlet.ServletRequest;
import javax.servlet.ServletResponse;
import javax.servlet.http.HttpServletRequest;

import javax.persistence.EntityManager;
import javax.persistence.EntityTransaction;

public class EMSupportFilter 
    implements Filter 
{

    protected FilterConfig filterConfig = null;

    public void destroy() {
        this.filterConfig = null;
    }

    public void doFilter(ServletRequest request, ServletResponse response, FilterChain chain)
        throws IOException, ServletException 
    {
        EntityManager em = EntryPoint.getEMF().createEntityManager();
        EntityManagerHolder.getInstance().setEM(em);
        System.out.println("EMSupportFilter.doFilter: start...");
        chain.doFilter(request, response);
        System.out.println("EMSupportFilter.doFilter: done.");
    }

    public void init(FilterConfig filterConfig) 
        throws ServletException 
    {
        this.filterConfig = filterConfig;
    }

}
