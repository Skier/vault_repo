package com.themidnightcoders.fbplugin.generator;

import java.io.FileWriter;
import java.io.FileReader;
import java.io.BufferedWriter;
import java.io.InputStreamReader;
import java.util.Map;
import java.io.StringReader;
import java.io.StringWriter;
import java.util.Map;

import org.apache.velocity.VelocityContext;
import org.apache.velocity.app.VelocityEngine;

public class WebORBVelocityCodeGenerator
    implements WebORBCodeGenerator
{
    protected String templateFilename = null;
    protected String destFilename = null;
    
    public WebORBVelocityCodeGenerator(String templateFilename, String destFilename) {
        this.templateFilename = templateFilename;
        this.destFilename = destFilename;
    }
    
    public void generate(String resultFilename, Map context)
        throws Exception
    {
        VelocityEngine ve = new VelocityEngine();
        try {
            ve.init();
        } catch (Exception e) {
            throw new RuntimeException("Cannot init velocity engine", e);
        }

        VelocityContext vc = new VelocityContext(context);

        String dest = (null != resultFilename) ? resultFilename : destFilename;
        BufferedWriter sw = new BufferedWriter(new FileWriter(dest));
        System.out.println("WebORBVelocityCodeGenerator.generate: resultFilename=" + resultFilename);
        InputStreamReader sr = new InputStreamReader(
            getClass().getClassLoader().getResourceAsStream(templateFilename));
        boolean result;
        try {
            result = ve.evaluate(vc, sw, "velocity.log", sr);
        } catch (Exception e) {
            throw new RuntimeException("Cannot load/parse velocity template", e);
        } finally {
            sr.close();
            sw.close();
        }
    }

}
