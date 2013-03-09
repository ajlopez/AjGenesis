<%@ page import="java.io.*" %>

<%
    String pageTitle=request.getParameter("title");
    String pagePrefix=request.getParameter("prefix");

    if (pageTitle==null)
        pageTitle="";

    if (pagePrefix==null)
        pagePrefix="";
%>

</td>
</tr>
</table>

<br>
<br>

<p>&nbsp;</p>


<p align="center" class="Footer">
<a href="<%=pagePrefix%>Home.do">Main</a>
<br>
<br>
Created using <a href="http://www.ajlopez.com/ajgenesis">AjGenesis</a>
</a>
</p>

</td>
</tr>
</table>

</BODY>
</HTML>

