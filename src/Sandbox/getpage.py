import urllib.request as ur

url = 'http://www.wowebook.org'
conn = ur.urlopen(url)
print (conn.status)

fout = open('wowebookorg.txt','wt')
data = conn.read()
print(data, file=fout)


