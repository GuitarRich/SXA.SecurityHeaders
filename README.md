# SXA.SecurityHeaders
Sitecore SXA Security Headers Module

[![Build status](https://ci.appveyor.com/api/projects/status/3o4of8tlkd83g2j7?svg=true)](https://ci.appveyor.com/project/GuitarRich/sxa-securityheaders)

Features
---
Adds response headers to your SXA site that allow you to control the following:

- Content Security Policy (CSP)
- HTTP Strict Transport Security (HSTS)
- X-Content-Type-Options
- X-Frame-Options
- X-XSS-Protection
- Referrer Policy


Getting Started 
---

- Download the packages from the releases or the Sitecore Market Place (link to follow).
- Install the package
- Install the module on the Tenant & the Site, it will create a basic security setup for you in your site.
- Navigate to `<your-site>\Settings\Securirty Headers` and modify the security policy for your needs.

For background and more details, you can read the [blog post](https://www.sitecorenutsbolts.net/2018/07/27/Sitecore-SXA-Using-HTTP-Headers-to-Secure-Your-Site/) about the module.


Check Your Score:
---

To check your sites security headers score, use [Mozilla Observatory](https://observatory.mozilla.org/) and add your sites url in. You can also validate your Content Security Policty using the [cspvalidator.org](https://www.cspvalidator.org/#url=https://www.cspvalidator.org/) site.
