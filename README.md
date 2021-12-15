----------------------------------------------------------Pasileidimas------------------------------------------------------
1)	https://github.com/Justuxs/CityPuzzleAPI 
2)	 *\source\repos\CityPuzzleAPI\.vs\CityPuzzleAPI\config\ – atsidaryti applicationhost
P.S Gali nerodyti per windows- todel reikia pakeisti nustatymus kad matytu hidden folderius  
3) Pakeisti kad binidng butu toks(155-156):
 
Sitos eilutes turi buti: (pakeiskite kas buvo I sitas=>)
    <bindings>
          <binding protocol="http" bindingInformation=":8080:localhost" />
          <binding protocol="http" bindingInformation=":8080:127.0.0.1" />
        </bindings>

4)Pakeisti kad binidng butu toks(163-167): 
Sitos eilutes turi buti: (pakeiskite kas buvo I sitas=>)
<bindings>
          <binding protocol="http" bindingInformation="*:26790:localhost" />
          <binding protocol="http" bindingInformation="*:26790:127.0.0.1" />
          <binding protocol="http" bindingInformation="*:26790:86.38.160.86" />
</bindings>
5)Paruninti api butent ant tokio(ne ant IIS)
  
6)pasikeisti db connString i norima- nuoroda-> CityPuzlleContext 15

----------------------------------------------------------Db keitimas--------------------------------------------------------

Db connString galima keisti ir remotely per post komanda  api/ChangeConectionString
Jei reikia paduoti Json objekta kuri sudaro conn: string- db sorce ir token: string -tokenas be kurio neleis pakesiti.
PS. dabartinis token-"CityPuzzle"



