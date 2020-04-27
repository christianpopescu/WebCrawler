
from urllib.request import urlopen
from bs4 import BeautifulSoup

html = urlopen ('http://www.wowebook.org')
bs = BeautifulSoup(html.read(),'html.parser')
nameList = bs.find_all('h2',{'class','post-title entry-title'})
for name in nameList:
    print (name.get_text())
articleList = bs.find_all('article')
for art in articleList:
    print(art.time.attrs['datetime'])
for art in articleList:
    ttl = art.find('h2',{'class','post-title entry-title'})
    print('Title: ')
    print(ttl.a.get_text())
    print('Link: ')
    print(ttl.a.attrs['href'])
    sm = art.find('div',{'class','entry excerpt entry-summary'})
    print('Summary')
    print (sm.p.get_text())

