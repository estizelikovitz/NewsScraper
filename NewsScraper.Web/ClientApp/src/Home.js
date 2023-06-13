import React, { useState, useEffect } from 'react';
import axios from 'axios';

const Home = () => {
    const [posts, setPosts] = useState([]);

    useEffect(() => {
        getPosts();
        console.log(posts);
    }, []);


        const getPosts = async () => {
            const { data } = await axios.get(`/api/lakewoodscoop/scrape`);
            setPosts(data);
        }


    return (
        <>
        <div className='container mt-5'>

                {!!posts.length && <div >

                                {posts.map((post, id) => {
                                    return <div className="card border-light mb-3 w-50 p-3 jumbotron" key={id}>
                                        <div className="card-header bg-transparent border-dark"><a href={post.link} target='_blank'>{post.title}</a></div>
                                        <div className="card-body text-dark">
                                            <h5 className="card-title"><img src={post.imageUrl} style={{width: 200}} /></h5>
                                            <p className="card-text">{post.text}</p>
                                        </div>
                                        <div className="card-footer bg-transparent border-dark">{post.comments}</div>
                                    </div>
                                })}
                                </div>

                }
                <br />
                <br />
                <br />

            </div>


        </>
    )
}

export default Home;