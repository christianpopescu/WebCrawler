
from urllib.request import urlopen
from bs4 import BeautifulSoup
from xml.etree.ElementTree import Element, SubElement, Comment, tostring, ElementTree


root = Element('BookList')


for i in range (1,10):
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
        book = SubElement(root,'book')
        ttl = art.find('h2',{'class','post-title entry-title'})
        title = SubElement(book,'Title').text = ttl.a.get_text()
        link = SubElement(book,'Link').text = ttl.a.attrs['href']
        dt = SubElement(book,'DateTime').text = art.time.attrs['datetime']
#        sm = art.find('div',{'class','entry excerpt entry-summary'})
#        sumary = SubElement(book,'Summary').text = sm.p.get_text()
#        print('Summary:', file=fout)
#        print (sm.p.get_text(), file=fout)

tree = ElementTree(root)
tree.write('Wowebook_books_list.xml')
