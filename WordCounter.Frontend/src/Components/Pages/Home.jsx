import './Home.css'
import { getWordDensity } from '../../Api/api'
import { useState } from "react";
import Toolbar from '../Controls/Toolbar/Toolbar'

export default function Home() {
    const [wordData, setWordData] = useState(null);
    const columnDrawer = (groupSize) => (
        <div>
            <div className='tableHeader'>
                <div className='wordsTable'>
                    <div className='wordsColumn'>
                        <h4>Word</h4>
                    </div>
                    <div className='wordsColumn'>
                        <h4>Count</h4>
                    </div>
                    <div className='wordsColumn'>
                        <h4>Percent</h4>
                    </div>
                </div>
            </div>
            { columnDataDrawer(groupSize) }
        </div>
    );

    const columnDataDrawer = (groupSize) => {
        if(!wordData) {
            return null;
        }

        return wordData.filter(x => x.groupSize == groupSize).map((x, i) => (
            <div key={i} className='row'>
                <div>
                    <p>{x.phrase}</p>
                </div>
                <div>
                    <p>{x.count}</p>
                </div>
                <div>
                    <p>{x.frequency}</p>
                </div>
            </div>
        ))}

    const wordTableDrawer = () => {
        return (
            <div>
                <div className='wordsTable'>
                    <div className='wordsColumn'>
                        {columnDrawer(1)}
                    </div>
                    <div className='wordsColumn'>
                        {columnDrawer(2)}
                    </div>
                    <div className='wordsColumn'>
                        {columnDrawer(3)}
                    </div>
                </div>
            </div>
        )
    };

    const reloadData = (uri, groupsCount, removeGrammars) => {
        getWordDensity({
            GroupsCount: groupsCount,
            Uri: uri,
            RemoveGrammars: removeGrammars
        })
            .then(x => setWordData(x));
    }

    return (
        <div>
            <div className='header'>
                <h2 className="title">TOP WORDS DENSITY</h2>
                <Toolbar onChange={(state) => {
                    console.log(state);
                    reloadData(state.url, state.groupsCount, state.removeGrammar)
                }} />
            </div>
            <div className='center'>
                { wordTableDrawer() }
            </div>
        </div>
    )
}