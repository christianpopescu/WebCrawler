
from urllib.request import urlopen
from bs4 import BeautifulSoup
from xml.etree.ElementTree import Element, SubElement, Comment, tostring


BookList = Element('BookList')
fout = open('wowebookorg_lst.txt','wt')


for i in range (1,6):
    html = urlopen ('http://www.wowebook.org/page/'+str(i))
    print ('Page: ' + str(i))
    bs = BeautifulSoup(html.read(),'html.parser')
    nameList = bs.find_all('h2',{'class','post-title entry-title'})
#    for name in nameList:
#        print (name.get_text(), file=fout)
    articleList = bs.find_all('article')
#    for art in articleList:
#        print(art.time.attrs['datetime'],file=fout)
    for art in articleList:
        ttl = art.find('h2',{'class','post-title entry-title'})
        print('Title: ', file=fout, end='')
		
        print(ttl.a.get_text(), file=fout)
        print('Link: ', file=fout, end='')
        print(ttl.a.attrs['href'], file=fout)
#        sm = art.find('div',{'class','entry excerpt entry-summary'})
#        print('Summary:', file=fout)
#        print (sm.p.get_text(), file=fout)

print (tostring(BookList), file=fout)
