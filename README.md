<h4>
<ul>
<li>
Am creeat un program in C# winforms ce rezolva un arbore folosing metoda min-max alfa-beta :D
</li>
 <li>
Puteti incerca sa rulati fisierul .exe din folderul 'Executable file' pentru a vedea cum functioneaza ( sau doar sa verificati codul sursa daca, evident,este riscant sa porniti un fisier .exe" )  
    </li>
</ul> </h4>

![Screenshot_5735](https://user-images.githubusercontent.com/91731551/138122747-1bd4a734-b588-4579-ae0f-b386457d1b9b.png)

![Screenshot_5734](https://user-images.githubusercontent.com/91731551/138122727-55b8c8e7-05c8-4545-a904-1fe5b420813a.png)

![Screenshot_5729](https://user-images.githubusercontent.com/91731551/138127988-4dabd79a-bc7d-4d99-b306-2fe452b91db8.png)

<br/>
<h3> Pentru exercitiul de la tema, programul afiseaza : </h3> <br/><br/>

*************************************************************************
Started processing node A with Alpha=-infinit and Beta=infinit, level=MAX
    Started processing node B with Alpha=-infinit and Beta=infinit, level=MIN
        Reached node E and returned cost 2
    From parent node B, changed child node E Beta value from infinit to 2
    From parent node B, changed child node F Beta value from infinit to 2
        Started processing node F with Alpha=-infinit and Beta=2, level=MAX
            Reached node K and returned cost 3
        Pruned node L because Beta=2 <= Alpha=3
        Found child nodes: [K=3]
        Stopped processing node F , new Alpha=3 and Beta=2 level=MAX
        *********************************
    Found child nodes: [E=2,F=3]
    Stopped processing node B , new Alpha=-infinit and Beta=2, level=MIN
    *********************************
From parent node A, changed child node B Alpha value from -infinit to 2
From parent node A, changed child node C Alpha value from -infinit to 2
From parent node A, changed child node D Alpha value from -infinit to 2
    Started processing node C with Alpha=2 and Beta=infinit, level=MIN
        Started processing node G with Alpha=-infinit and Beta=infinit, level=MAX
            Started processing node M with Alpha=-infinit and Beta=infinit, level=MIN
                Reached node Q and returned cost 1
            From parent node M, changed child node Q Beta value from infinit to 1
            From parent node M, changed child node R Beta value from infinit to 1
                Reached node R and returned cost 10
            Found child nodes: [Q=1,R=10]
            Stopped processing node M , new Alpha=-infinit and Beta=1, level=MIN
            *********************************
        From parent node G, changed child node M Alpha value from -infinit to 1
        From parent node G, changed child node N Alpha value from -infinit to 1
            Reached node N and returned cost 7
        From parent node G, changed child node M Alpha value from 1 to 7
        From parent node G, changed child node N Alpha value from 1 to 7
        Found child nodes: [M=1,N=7]
        Stopped processing node G , new Alpha=7 and Beta=infinit level=MAX
        *********************************
    From parent node C, changed child node G Beta value from infinit to 7
    From parent node C, changed child node H Beta value from infinit to 7
        Reached node H and returned cost 2
    Found child nodes: [G=7,H=2]
    Stopped processing node C , new Alpha=2 and Beta=2, level=MIN
    *********************************
    Started processing node D with Alpha=2 and Beta=infinit, level=MIN
        Reached node I and returned cost 1
    Pruned node J because Beta=1 <= Alpha=2
    Found child nodes: [I=1]
    Stopped processing node D , new Alpha=2 and Beta=1, level=MIN
    *********************************
Found child nodes: [B=2,C=2,D=1]
Stopped processing node A , new Alpha=2 and Beta=infinit level=MAX
*********************************

Final values of all nodes :
A=2 (Alpha=2,Beta=infinit,Cost=unknown,Level=MAX)
B=2 (Alpha=2,Beta=2,Cost=unknown,Level=MIN)
E=2 (Alpha=-infinit,Beta=2,Cost=2,Level=MAX)
F=3 (Alpha=3,Beta=2,Cost=unknown,Level=MAX)
K=3 (Alpha=-infinit,Beta=infinit,Cost=3,Level=MIN)
L=0 (Alpha=-infinit,Beta=infinit,Cost=0,Level=MIN)
C=2 (Alpha=2,Beta=2,Cost=unknown,Level=MIN)
G=7 (Alpha=7,Beta=7,Cost=unknown,Level=MAX)
M=1 (Alpha=7,Beta=1,Cost=unknown,Level=MIN)
Q=1 (Alpha=-infinit,Beta=1,Cost=1,Level=MAX)
R=10 (Alpha=-infinit,Beta=1,Cost=10,Level=MAX)
N=7 (Alpha=7,Beta=infinit,Cost=7,Level=MIN)
H=2 (Alpha=-infinit,Beta=7,Cost=2,Level=MAX)
D=1 (Alpha=2,Beta=1,Cost=unknown,Level=MIN)
I=1 (Alpha=-infinit,Beta=infinit,Cost=1,Level=MAX)
J=unknown (Alpha=-infinit,Beta=infinit,Cost=unknown,Level=MAX)
O=2 (Alpha=-infinit,Beta=infinit,Cost=2,Level=MIN)
P=20 (Alpha=-infinit,Beta=infinit,Cost=20,Level=MIN)
*************************************************************************
